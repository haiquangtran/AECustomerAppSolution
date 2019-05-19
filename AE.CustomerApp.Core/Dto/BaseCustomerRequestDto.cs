using AE.CustomerApp.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AE.CustomerApp.Core.Dto
{
    [JsonObject(Title = "customer_request")]
    public abstract class BaseCustomerRequestDto
    {
        /// <summary>
        /// Customer's first name
        /// </summary>
        /// <example>
        /// Bob
        /// </example>
        [StringLength(255)]
        [JsonProperty("first_name", Required = Required.Always)]
        public string FirstName { get; set; }

        /// <summary>
        /// Customer's last name
        /// </summary>
        /// <example>
        /// Marley
        /// </example>
        [StringLength(255)]
        [JsonProperty("last_name", Required = Required.Always)]
        public string LastName { get; set; }
        
        /// <summary>
        /// Customer's date of birth in the format of YYYY-MM-DD
        /// </summary>
        /// <example>
        /// 1993-12-22
        /// </example>
        [JsonProperty("date_of_birth", Required = Required.Always)]
        public DateTime DateOfBirth { get; set; }
    }
}
