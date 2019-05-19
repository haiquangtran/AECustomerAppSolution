using AE.CustomerApp.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AE.CustomerApp.Core.Dto
{
    [JsonObject(Title = "update_customer_request")]
    public class UpdateCustomerRequestDto : BaseCustomerRequestDto
    {
    }
}
