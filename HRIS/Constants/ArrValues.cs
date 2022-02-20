using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRIS.Constants
{
    public class ArrValues
    {
        public static string[] Titles = new string[] { "Ms", "Miss", "Mrs", "Mr", "Dr", "Prof", "Hon", "Rev", "Fr" };
        public static string[] Genders = new string[] { "All", "Male", "Female" };
        public static string[] MaritalStatuses = new string[] { "Single", "Married", "Divorced", "Windowed" };
        public static string[] DayTypes = new string[] { "Full Day", "Half Day", "Non-working Day" };
        public static string[] WeekDays = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        public static string[] LeaveEntilementTypes = new string[] { "Annual Entitlement", "Leave Adjustment", "Opening Balance" };
        public static string[] DayTimes = new string[] { "AM", "PM" };
    }
}
