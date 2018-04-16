namespace Data
{
	public abstract class BaseDocument
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public DocumentType DocumentType { get; set; }
	}

	public enum DocumentType
	{
		BaseDocument,
		OtherDocument
	}
}