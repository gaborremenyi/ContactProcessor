namespace ContactProcessor.Models
{
    public class NewContactViewModel : ContactViewModel
    {
        public string FileName { get; set; }

        public NewContactViewModel() { }

        public NewContactViewModel(string firstName, string lastName, string phoneNumber, string email, string fileName) 
            : base(firstName, lastName, phoneNumber, email)
        {
            FileName = fileName;
        }
    }
}