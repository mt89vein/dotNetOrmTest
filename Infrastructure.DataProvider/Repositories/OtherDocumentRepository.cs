﻿using Domain;
using Domain.FetchStrategies;

namespace Infrastructure.DataProvider.Repositories
{
	public class OtherDocumentRepository : LinqRepository<OtherDocument, OtherDocumentDto, ISpecification<OtherDocumentDto>>,
		IOtherDocumentRepository
	{
		public OtherDocumentRepository(ApplicationContext context)
			: base(context)
		{
		}
	}
}