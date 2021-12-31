using System;

namespace GenricFrame.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public Guid EmployeeId { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public Guid CompanyId { get; set; }
    }
}
