namespace CRUDFunctionality.api.Models
{
    public class UpdateContactRequest
    {
       
        public string? name
        {
            get;
            set;
        }
        public string? email
        {
            get;
            set;
        }
        public long phone
        {
            get;
            set;
        }
        public string? address
        {
            get;
            set;
        }
    }
}
