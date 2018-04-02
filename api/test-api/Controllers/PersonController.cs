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

		[HttpGet("{id}")]

		public Person Get(int id) {
			return new Person().Read( id );
		}

		[HttpPost]
		public void Post([FromBody]dynamic value) {
			new Person((string) value.name,(string) value.password, (string) value.email).Create();
		}

		[HttpPut("{id}")]
		public void Put(int id, [FromBody]dynamic value) {
			new Person((string) value.name, (string) value.password, (string) value.email).Update(id);
		}

		[HttpDelete("{id}")]
		public void Delete(int id) {
			new Person().Delete(id);
    	}

	}
}