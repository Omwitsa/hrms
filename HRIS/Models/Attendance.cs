using System;

namespace HRIS.Models
{
    public class Attendance
    {
        public Guid Id { get; set; }
        public string EmpNo { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? Time { get; set; }
    }
}
