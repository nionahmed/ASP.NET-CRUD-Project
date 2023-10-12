using System.ComponentModel.DataAnnotations;

namespace Employee360.Web.Models
{
    public class EmpOfficialInfo
    {
        [Key]
        public int Id { get; set; }
        public string? Post { get; set; }
        public int Salary { get; set;}
        public int Incperiod { get; set; }
    }
}
