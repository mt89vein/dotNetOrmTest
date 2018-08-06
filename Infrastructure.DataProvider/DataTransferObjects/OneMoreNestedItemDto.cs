namespace Infrastructure.DataProvider
{
    public partial class OneMoreNestedItemDto
    {
        public string OneMoreNestedItemName { get; set; }

        public int NestedItemId { get; set; }

        public virtual NestedItemDto NestedItemDto { get; set; }

        public bool Deleted { get; set; }

        public int Id { get; set; }
    }
}