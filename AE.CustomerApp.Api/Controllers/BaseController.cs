using AE.CustomerApp.Core;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AE.CustomerApp.Api.Controllers
{
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        protected IMapper _mapper { get; private set; }
        protected AppSettingsConfiguration _appSettings { get; private set; }

        public BaseController(IMapper mapper, IOptions<AppSettingsConfiguration> appSettings)
        {
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
    }
}

