using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;
using test_api.Helpers;

namespace test_api.Controllers
{
    public class LoginController : Controller
    {
        [Route("api/[controller]")]

        [HttpGet]
        public Person Get()
        {
            var data = Tokendata.Get(Request);
            List<Person> p = new Person().List();
            Person person = new Person();
            Int32 index = p.FindIndex( item => {
                return item.Email.Equals(data.UserEmail) && item.Password.Equals(data.UserPassword);
            });
            Console.WriteLine(index);
            if(index < 0)
                return null;
            person.Id = p[index].Id;
            return person;
        }
    }
}