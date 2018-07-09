using System;

namespace Domain
{
    public class PublicationEvent
    {
        public PublicationEvent(int? userId, DateTime? date)
        {
            UserId = userId;
            Date = date;
        }

        public int? UserId { get; }

        public DateTime? Date { get; }
    }
}