using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AE.CustomerApp.Core;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AE.CustomerApp.Api.Controllers
{
    public class CustomerController : BaseController
    {
        public CustomerController(IMapper mapper, IOptions<AppSettingsConfiguration> appSettings) : base(mapper, appSettings)
        {
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
