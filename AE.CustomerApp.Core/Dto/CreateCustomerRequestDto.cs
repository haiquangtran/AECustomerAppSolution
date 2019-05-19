using AE.CustomerApp.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AE.CustomerApp.Core.Dto
{
    [JsonObject(Title = "create_customer_request")]
    public class CreateCustomerRequestDto : BaseCustomerRequestDto
    {
    }
}
