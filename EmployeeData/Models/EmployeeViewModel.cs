using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeData.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Date of Birth")]
        public DateTime DOB { get; set; }
        [DisplayName("Designation")]
        public string Designation { get; set; }
        [DisplayName("Experience")]
        public double Experiance { get; set; }
        public double Salary { get; set; }
        [DisplayName("Name")]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
