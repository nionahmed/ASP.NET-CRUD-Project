using System.ComponentModel.DataAnnotations;

namespace CRUDFunctionality.api.Models
{
    public class AddPersonalDataDbModel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Dob { get; set; }

    }
}
