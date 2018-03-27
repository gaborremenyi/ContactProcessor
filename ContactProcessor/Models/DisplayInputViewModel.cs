using System.Collections.Generic;

namespace ContactProcessor.Models
{
    public class DisplayInputViewModel
    {
        public string FileName { get; set; }

        public List<ContactViewModel> Contacts { get; set; }
    }
}