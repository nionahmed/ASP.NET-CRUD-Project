using System.ComponentModel.DataAnnotations;

namespace CRUDFunctionality.api.Models
{
    public class AddOfficialDataDbModel
    {
        [Key]
        public int Id { get; set; }
        public string? Post { get; set; }
        public int Salary { get; set; }
        public int Incperiod { get; set; }

    }
}
