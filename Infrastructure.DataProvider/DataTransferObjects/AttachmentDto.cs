using System.Collections.Generic;

namespace Infrastructure.DataProvider
{
    public partial class AttachmentDto
    {
        public AttachmentDto()
        {
            AttachmentLinkDtos = new HashSet<AttachmentLinkDto>();
        }

        public string Path { get; set; }

        public ICollection<AttachmentLinkDto> AttachmentLinkDtos { get; set; }

        public bool Deleted { get; set; }

        public int Id { get; set; }
    }
}