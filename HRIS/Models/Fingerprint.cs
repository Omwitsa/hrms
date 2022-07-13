using System;

namespace HRIS.Models
{
    public class Fingerprint
    {
        public Guid Id { get; set; }
        public string EmpNo { get; set; }
        public string Template { get; set; }
        public string Memo { get; set; }
    }
}
