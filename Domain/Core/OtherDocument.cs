using System.Collections.Generic;

namespace Domain
{
	public class OtherDocument : Document
	{
		public OtherDocument(int id, string name, string testName, List<Attachment> attachments, bool deleted)
			: base(id, name, attachments, deleted)
		{
			TestName = testName;
		}

		public string TestName { get; }
	}
}