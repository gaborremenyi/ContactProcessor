namespace ContactProcessor.Models
{
    public class ContactViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public ContactViewModel() { }

        public ContactViewModel(string csvContact)
        {
            var csvContactValues = csvContact.Split(',');
            FirstName = csvContactValues[0];
            LastName = csvContactValues[1];
            PhoneNumber = csvContactValues[2];
            Email = csvContactValues[3];
        }

        public ContactViewModel(string fileName, string lastName, string phoneNumber, string email)
        {
            FirstName = fileName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public string ToCSVString()
        {
            return this.FirstName + "," + this.LastName + "," + this.PhoneNumber + "," + this.Email + "\n";
        }
    }
}