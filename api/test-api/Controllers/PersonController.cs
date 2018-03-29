using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace test_api.Controllers {
	[Route("api/[controller]")]
	public class PersonController : Controller {

		[HttpGet]
		public IEnumerable<Person> Get() {
			return new Person().List();
		}

		[HttpPost]
		public void Post([FromBody]dynamic value) {
			new Person((string) value.name,(string) value.password, (string) value.email).Create();
		}

		[HttpPut("{id}")]
		public void Put(int id, [FromBody]dynamic value) {

		}

		[HttpDelete("{id}")]
		public void Delete(int id) {
    
    }

	}
}