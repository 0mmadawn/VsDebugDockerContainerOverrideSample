using System;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace debugDockerComposeOverrideSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : Controller
    {
        [HttpGet]
        public string Get()
        {
            // access db and get some data!

            var connectionString = Environment.GetEnvironmentVariable("SampleReadOnlyConnection");

            User user;
            using (IDbConnection con = new MySqlConnection(connectionString))
            {
                user = con.QueryFirst<User>("SELECT U.id, U.name FROM Users U LIMIT 1");
            }
            return $"{user.Id}: {user.Name}";
        }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}