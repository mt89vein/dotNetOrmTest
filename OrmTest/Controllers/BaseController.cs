using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace OrmTest.Controllers
{
	[Route("api/[controller]")]
	public class BaseController : Controller
	{
		protected readonly string ConnectionString;
		public BaseController()
		{
			ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OrmTest;Integrated Security=True;";
		}
	}
}