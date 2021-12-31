using System;

namespace GenricFrame.Models
{
    public class Company
    {
        public int Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
    }
}
