using System;

namespace Domain.Core
{
    /// <summary>
    /// Интерфейс для публикуемых документов
    /// </summary>
    public interface IPublishable
    {
        PublishState? State { get;  } // computed

        PublicationEvent PublicationEvent { get;  }

        //PublicationRequestEvent PublicationRequestEvent { get; set; }
    }
}