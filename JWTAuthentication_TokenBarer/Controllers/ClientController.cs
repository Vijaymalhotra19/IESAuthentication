using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment_3_JWT.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Assignment_3_JWT.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        

        private readonly ILogger<ClientController> _logger;

        private readonly List<Client> client
            = new List<Client>()
        {
            new Client{ Id = 1, Name = "Vijay", Company= "SAE", SalesScore= 120 },
            new Client { Id = 2, Name = "Dinesh", Company= "Delloite", SalesScore= 140 },
            new Client { Id = 3, Name = "Rahul", Company= "Accenture", SalesScore= 240 },
            new Client{ Id = 4, Name = "Sagar", Company= "Kpmg", SalesScore= 360 }
        };

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Client> Get()
        {
            return client;
        }

        [HttpGet("{score:int}")]
        public Client GetOne(int score)
        {
            return client.SingleOrDefault(x => x.SalesScore ==score);
        }
    }
}

