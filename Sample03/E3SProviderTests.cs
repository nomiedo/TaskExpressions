﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample03.E3SClient.Entities;
using Sample03.E3SClient;
using System.Configuration;
using System.Linq;

namespace Sample03
{
	[TestClass]
	public class E3SProviderTests
	{
		[TestMethod]
		public void WithoutProvider()
		{
			var client = new E3SQueryClient(ConfigurationManager.AppSettings["user"] , ConfigurationManager.AppSettings["password"]);
			var res = client.SearchFTS<EmployeeEntity>(new []{ "workstation:(EPRUIZHW0249)" }, 0, 1);

			foreach (var emp in res)
			{
				Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
			}
		}

		[TestMethod]
		public void WithoutProviderNonGeneric()
		{
			var client = new E3SQueryClient(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);
			var res = client.SearchFTS(typeof(EmployeeEntity), new[] { "workstation:(EPRUIZHW0249)" }, 0, 10);

			foreach (var emp in res.OfType<EmployeeEntity>())
			{
				Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
			}
		}

        // methot for testing

		[TestMethod]
		public void WithProvider()
		{
			var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

			foreach (var emp in employees.Where(e => e.workstation == "EPRUIZHW0249"))
			{
				Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
			}
        }

	    [TestMethod]
	    public void WithProviderReverse()
	    {
	        var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

	        foreach (var emp in employees.Where(e => "EPRUIZHW0249" == e.workstation))
	        {
	            Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
	        }
	    }

        [TestMethod]
	    public void WithProviderAndStartsWith()
	    {
	        var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

	        foreach (var emp in employees.Where(e => e.workstation.StartsWith("EPRUIZHW024")))
	        {
	            Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
	        }
	    }

	    [TestMethod]
	    public void WithProviderAndEndsWith()
	    {
	        var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

	        foreach (var emp in employees.Where(e => e.workstation.EndsWith("PRUIZHW0249")))
	        {
	            Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
	        }
	    }

	    [TestMethod]
	    public void WithProviderAndContains()
	    {
	        var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

	        foreach (var emp in employees.Where(e => e.workstation.Contains("RUIZHW024")))
	        {
	            Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
	        }
	    }

	    [TestMethod]
	    public void WithProviderWithAnd()
	    {
	        var employees = new E3SEntitySet<EmployeeEntity>(ConfigurationManager.AppSettings["user"], ConfigurationManager.AppSettings["password"]);

	        foreach (var emp in employees.Where(e => e.workstation == "EPRUIZHW0249" && e.nativename == "Михаил Романов"))
	        {
	            Console.WriteLine("{0} {1}", emp.nativename, emp.startworkdate);
	        }
	    }
    }
}
