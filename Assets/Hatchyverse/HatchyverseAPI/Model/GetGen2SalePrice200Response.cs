/*
 * hatchyverse-api
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0.0
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */


using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using OpenAPIDateConverter = HatchyverseAPI.Client.OpenAPIDateConverter;

namespace HatchyverseAPI.Model
{
    /// <summary>
    /// GetGen2SalePrice200Response
    /// </summary>
    [DataContract(Name = "GetGen2SalePrice_200_response")]
    public partial class GetGen2SalePrice200Response
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetGen2SalePrice200Response" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected GetGen2SalePrice200Response() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="GetGen2SalePrice200Response" /> class.
        /// </summary>
        /// <param name="price">price (required).</param>
        public GetGen2SalePrice200Response(double price = default(double))
        {
            this.Price = price;
        }

        /// <summary>
        /// Gets or Sets Price
        /// </summary>
        [DataMember(Name = "price", IsRequired = true, EmitDefaultValue = true)]
        public double Price { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class GetGen2SalePrice200Response {\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

    }

}
