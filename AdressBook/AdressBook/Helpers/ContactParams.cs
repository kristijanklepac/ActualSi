using System;
namespace AdressBook.Helpers
{
    public class ContactParams
    {
        private const int MaxPageSize = 50; // ne dozvoli da netko posalje prevelik broj za dohvat iz API-a npr. 1 000 000 contacts
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; } // objasnjenje gore
        }

        public string SearchText { get; set; } = "";
    }
}
