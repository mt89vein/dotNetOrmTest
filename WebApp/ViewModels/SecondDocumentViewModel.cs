using System;
using Domain;

namespace OrmTest.ViewModels
{
    public class SecondDocumentViewModel
    {
        public SecondDocumentViewModel(SecondDocument document)
        {
            Id = document.Id;
            Name = document.Name;
            CreatedAt = document.CreatedAt;
            DocumentSigner = document.DocumentSigner;
        }

        public SecondDocumentViewModel()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedAt { get; set; }

        public string DocumentSigner { get; set; }

        public SecondDocument GetModel()
        {
            return new SecondDocument(Id, Name, DocumentSigner, CreatedAt);
        }
    }
}