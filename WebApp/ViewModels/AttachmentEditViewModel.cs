using Domain;

namespace WebApp.ViewModels
{
    public class AttachmentEditViewModel
    {
        public AttachmentEditViewModel(Attachment attachment)
        {
            Id = attachment.Id;
            Deleted = attachment.Deleted;
            Path = attachment.Path;
        }

        public AttachmentEditViewModel()
        {
        }

        public int Id { get; set; }

        public bool Deleted { get; set; }

        public string Path { get; set; }

        public Attachment GetModel()
        {
            return new Attachment(Path, Id, Deleted);
        }
    }
}