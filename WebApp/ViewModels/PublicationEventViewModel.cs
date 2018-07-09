using System;
using Domain;

namespace OrmTest.ViewModels
{
    public class PublicationEventViewModel
    {
        public PublicationEventViewModel(PublicationEvent publicationEvent)
        {
            UserId = publicationEvent.UserId;
            UserName = publicationEvent.UserId.ToString(); // some logic to find UserName by UserId
            Date = publicationEvent.Date;
        }

        public PublicationEventViewModel()
        {
        }

        public string UserName { get; set; }

        public int? UserId { get; set; }

        public DateTime? Date { get; set; }
    }
}