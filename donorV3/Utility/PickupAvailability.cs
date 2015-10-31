using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using VOA.Models;

namespace VOA.Utility
{ 
    /// <summary>
    /// Summary description for Class1
    /// </summary>
    public class PickupAvailability
    {
        private int PICKUP_OPTION_COUNT = 10;           // Number of pickup dates to calculate
        private int MAX_PICKUP_PER_DAY = 75;            // Default - Actual value extracted from database
        private List<Holiday> Holidays = null;          // List of holidays that should be excluded from pickup candidate list
        private List<ZipCode> Zipcodes = null;          // List of zip codes
        private List<DateTime> CandidateDates = null;   // List of candidate pickup dates for specified zip code
        private List<DateTime> PickupDates = null;      // List of pickup dates for specified zip code

        // Constructor for PickupAvailability class
	    public PickupAvailability()
	    {
            HolidayDBContext hDB = new HolidayDBContext();            

            // Initialize list of candidate pickup dates
            CandidateDates = new List<DateTime>();

            // Initialize list of pickup dates
            PickupDates = new List<DateTime>();

		    // Populate the list of Holidays from the database
            Holidays = hDB.Holidays.ToList();            
	    }

        // Return a list of pickup dates associated with the provided zipcode
        public List<DateTime> GetAvailablePickupDates(string zipCode)
        {
            ZipCodeDBContext zDB = new ZipCodeDBContext();

            // Reset list of pickup dates
            CandidateDates.Clear();

            // Populate the ZipcodeSchedule from the database for the designated zipCode
            var zips = from z in zDB.ZipCodes
                       where z.Code.Contains(zipCode)
                       select z;

            Zipcodes = zips.ToList();

            PopulateDates();

            PruneHolidays();

            return PickupDates;
        }

        // Scan all zipcode schedules and populate candidate dates
        private void PopulateDates()
        {        
            DateTime Today = DateTime.Now;                                  // Today's date
            Today = new DateTime(Today.Year, Today.Month, Today.Day);

            foreach (ZipCode z in Zipcodes)
            {
                if (z.HasPickup())
                {
                    if (z.Monday == true)
                    {
                        PopulateCandidateDates(Today, DayOfWeek.Monday, z.WeekOfMonth);
                    }
                    if (z.Tuesday == true)
                    {
                        PopulateCandidateDates(Today, DayOfWeek.Tuesday, z.WeekOfMonth);
                    }
                    if (z.Wednesday == true)
                    {
                        PopulateCandidateDates(Today, DayOfWeek.Wednesday, z.WeekOfMonth);
                    }
                    if (z.Thursday == true)
                    {
                        PopulateCandidateDates(Today, DayOfWeek.Thursday, z.WeekOfMonth);
                    }
                    if (z.Friday == true)
                    {
                        PopulateCandidateDates(Today, DayOfWeek.Friday, z.WeekOfMonth);
                    }
                    if (z.Saturday == true)
                    {
                        PopulateCandidateDates(Today, DayOfWeek.Saturday, z.WeekOfMonth);
                    }
                    if (z.Sunday == true)
                    {
                        PopulateCandidateDates(Today, DayOfWeek.Sunday, z.WeekOfMonth);
                    }
                }
            }

            PruneHolidays();
        }

        private void PopulateCandidateDates(DateTime startDate, DayOfWeek targetDOW, int frequency)
        {
            DateTime AvailablePickupDT = DateTime.Now;

            if (frequency == 0)
            {
                AvailablePickupDT = GetNextDateForDOW(startDate, targetDOW);
            }
            else
            {
                AvailablePickupDT = GetNextDateForDOW(startDate, targetDOW, frequency);
            }

            // Add initial date
            CandidateDates.Add(AvailablePickupDT);

            // Calculate next N dates.  We generate more pickup date options so any holidays that are
            // pruned out don't bring the total list of dates below the desired count.
            for (int i = 1; i < PICKUP_OPTION_COUNT * 2; i++)
            {            
                if (frequency == 0)
                {
                    AvailablePickupDT = AvailablePickupDT.AddDays(7.0);
                }
                else
                {
                    AvailablePickupDT = GetNextDateForDOW(AvailablePickupDT, targetDOW, frequency);
                }

                CandidateDates.Add(AvailablePickupDT);
            }

            return;
        }

        protected DateTime GetNextDateForDOW(DateTime startDate, DayOfWeek targetDOW)
        {
            DateTime PotentialDT = startDate;

            // Iterate across the next 7 days
            for (int i = 1; i < 8; i++)
            {
                PotentialDT = PotentialDT.AddDays(1.0);
                if (PotentialDT.DayOfWeek == targetDOW)
                {
                    return PotentialDT;
                }
            }

            throw new Exception("Unable to get next date for " + targetDOW + ", starting on " + startDate.ToShortDateString());
        }

        protected DateTime GetNextDateForDOW(DateTime startDate, DayOfWeek targetDOW, int frequency)
        {
            DateTime NextMonth = startDate.AddMonths(1);
            DateTime FirstWeekOfThisMonth = new DateTime(startDate.Year, startDate.Month, 1);
            DateTime FirstWeekOfNextMonth = new DateTime(NextMonth.Year, NextMonth.Month, 1);

            if (FirstWeekOfThisMonth.DayOfWeek != targetDOW)
            {
                FirstWeekOfThisMonth = GetNextDateForDOW(FirstWeekOfThisMonth, targetDOW);
            }

            if (FirstWeekOfNextMonth.DayOfWeek != targetDOW)
            {
                FirstWeekOfNextMonth = GetNextDateForDOW(FirstWeekOfNextMonth, targetDOW);
            }

            switch (frequency)
            {
                case 1:
                    if (FirstWeekOfThisMonth <= startDate)
                    {
                        // Already missed this month's pickup date, return first week of next month
                        return FirstWeekOfNextMonth;
                    }
                    else
                    {
                        return FirstWeekOfThisMonth;
                    }
                    break;
                case 2:
                    if (FirstWeekOfThisMonth.AddDays(7) <= startDate)
                    {
                        // Already missed this month's pickup date, return second week of next month
                        return FirstWeekOfNextMonth.AddDays(7);
                    }
                    else
                    {
                        return FirstWeekOfThisMonth.AddDays(7);
                    }
                    break;
                case 3:
                    if (FirstWeekOfThisMonth.AddDays(14) <= startDate)
                    {
                        // Already missed this month's pickup date, return third week of next month
                        return FirstWeekOfNextMonth.AddDays(14);
                    }
                    else
                    {
                        return FirstWeekOfThisMonth.AddDays(14);
                    }
                    break;
                case 4:
                    if (FirstWeekOfThisMonth.AddDays(21) <= startDate)
                    {
                        // Already missed this month's pickup date, return fourth week of next month
                        return FirstWeekOfNextMonth.AddDays(21);
                    }
                    else
                    {
                        return FirstWeekOfThisMonth.AddDays(21);
                    }
                    break;
                case 5:
                    if (FirstWeekOfThisMonth <= startDate)
                    {
                        if (FirstWeekOfThisMonth.AddDays(14) <= startDate)
                        {
                            // Can't make it this month, need to go to first week of next month
                            return FirstWeekOfNextMonth;
                        }
                        else
                        {
                            // We still can schedule pickup for third DOW this month!
                            return FirstWeekOfThisMonth.AddDays(14);
                        }
                    }
                    else
                    {
                        // We still can schedule pickup for first DOW this month!
                        return FirstWeekOfThisMonth;
                    }
                    break;
                case 6:
                    if (FirstWeekOfThisMonth.AddDays(7) <= startDate)
                    {
                        if (FirstWeekOfThisMonth.AddDays(21) <= startDate)
                        {
                            // Can't make it this month, need to go to second week of next month
                            return FirstWeekOfNextMonth.AddDays(7);
                        }
                        else
                        {
                            // We still can schedule pickup for fourth DOW this month!
                            return FirstWeekOfThisMonth.AddDays(21);
                        }
                    }
                    else
                    {
                        // We still can schedule pickup for second DOW this month!
                        return FirstWeekOfThisMonth.AddDays(7);
                    }
                    break;
            }

            throw new Exception("Unable to calculate first date for scheduling pickup! Frequency=" + frequency);
        }

        private void PruneHolidays()
        {                                    
            CandidateDates.Sort();         // Sort candidate list of pickup dates

            foreach (DateTime dt in CandidateDates)
            {                
                // Check to see if date has already been used
                if (PickupDates.Exists(d => d.Equals(dt)))
                {
                    continue;
                }

                // If date not in holiday list and pickup count < threshold, add it to selection list
                if (Holidays.Exists(h => h.Date.Equals(dt)))
                {
                    continue;
                }

                PickupDates.Add(dt);                
            }
        }
    }
}