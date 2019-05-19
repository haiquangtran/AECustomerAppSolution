using AE.CustomerApp.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AE.CustomerApp.Core.Dto
{
    [JsonObject(Title = "Customer")]
    public class CustomerDto
    {
        /// <summary>
        /// Customer Id
        /// </summary>
        /// <example>
        /// 101
        /// </example>
        [JsonProperty("id")]
        public int Id { get; set; }
        
        /// <summary>
        /// Customer First Name
        /// </summary>
        /// <example>
        /// Bob
        /// </example>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Customer Last Name
        /// </summary>
        /// <example>
        /// Marley
        /// </example>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Date of birth in format YYYY-MM-DD
        /// </summary>
        /// <example>
        /// 1993-12-22
        /// </example>
        [JsonProperty("date_of_birth")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Created date in date time format
        /// </summary>
        /// <example>
        /// 2019-05-19T12:38:37.367Z
        /// </example>
        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Created date in date time format
        /// </summary>
        /// <example>
        /// 2019-05-19T12:38:37.367Z
        /// </example>
        [JsonProperty("updated_date")]
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// Customer Full Name
        /// </summary>
        /// <example>
        /// Bob Marley
        /// </example>
        [JsonProperty("full_name")]
        public string FullName => $"{FirstName} {LastName}";
    }
}
