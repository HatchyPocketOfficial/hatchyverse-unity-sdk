/*
 * masters-api
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
    /// MintMastersItemRequest
    /// </summary>
    [DataContract(Name = "MintMastersItem_request")]
    public partial class MintMastersItemRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MintMastersItemRequest" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected MintMastersItemRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="MintMastersItemRequest" /> class.
        /// </summary>
        /// <param name="amount">amount (required).</param>
        /// <param name="receiver">receiver (required).</param>
        /// <param name="itemId">itemId (required).</param>
        /// <param name="clientId">clientId (required).</param>
        /// <param name="apiKey">apiKey (required).</param>
        public MintMastersItemRequest(double amount = default(double), string receiver = default(string), double itemId = default(double), string clientId = default(string), string apiKey = default(string))
        {
            this.Amount = amount;
            // to ensure "receiver" is required (not null)
            if (receiver == null)
            {
                throw new ArgumentNullException("receiver is a required property for MintMastersItemRequest and cannot be null");
            }
            this.Receiver = receiver;
            this.ItemId = itemId;
            // to ensure "clientId" is required (not null)
            if (clientId == null)
            {
                throw new ArgumentNullException("clientId is a required property for MintMastersItemRequest and cannot be null");
            }
            this.ClientId = clientId;
            // to ensure "apiKey" is required (not null)
            if (apiKey == null)
            {
                throw new ArgumentNullException("apiKey is a required property for MintMastersItemRequest and cannot be null");
            }
            this.ApiKey = apiKey;
        }

        /// <summary>
        /// Gets or Sets Amount
        /// </summary>
        [DataMember(Name = "amount", IsRequired = true, EmitDefaultValue = true)]
        public double Amount { get; set; }

        /// <summary>
        /// Gets or Sets Receiver
        /// </summary>
        [DataMember(Name = "receiver", IsRequired = true, EmitDefaultValue = true)]
        public string Receiver { get; set; }

        /// <summary>
        /// Gets or Sets ItemId
        /// </summary>
        [DataMember(Name = "itemId", IsRequired = true, EmitDefaultValue = true)]
        public double ItemId { get; set; }

        /// <summary>
        /// Gets or Sets ClientId
        /// </summary>
        [DataMember(Name = "clientId", IsRequired = true, EmitDefaultValue = true)]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or Sets ApiKey
        /// </summary>
        [DataMember(Name = "apiKey", IsRequired = true, EmitDefaultValue = true)]
        public string ApiKey { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class MintMastersItemRequest {\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  Receiver: ").Append(Receiver).Append("\n");
            sb.Append("  ItemId: ").Append(ItemId).Append("\n");
            sb.Append("  ClientId: ").Append(ClientId).Append("\n");
            sb.Append("  ApiKey: ").Append(ApiKey).Append("\n");
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