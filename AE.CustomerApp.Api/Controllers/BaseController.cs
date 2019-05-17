using AE.CustomerApp.Core;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AE.CustomerApp.Api.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMapper Mapper;
        protected readonly AppSettingsConfiguration AppSettings;

        public BaseController(IMapper mapper, IOptions<AppSettingsConfiguration> appSettings)
        {
            Mapper = mapper;
            AppSettings = appSettings.Value;
        }
    }
}

