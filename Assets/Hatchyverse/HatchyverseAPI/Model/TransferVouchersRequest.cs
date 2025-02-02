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
    /// TransferVouchersRequest
    /// </summary>
    [DataContract(Name = "TransferVouchers_request")]
    public partial class TransferVouchersRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransferVouchersRequest" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected TransferVouchersRequest() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="TransferVouchersRequest" /> class.
        /// </summary>
        /// <param name="receiverEmail">receiverEmail (required).</param>
        /// <param name="voucherAmounts">voucherAmounts (required).</param>
        /// <param name="voucherIds">voucherIds (required).</param>
        public TransferVouchersRequest(string receiverEmail = default(string), List<double> voucherAmounts = default(List<double>), List<string> voucherIds = default(List<string>))
        {
            // to ensure "receiverEmail" is required (not null)
            if (receiverEmail == null)
            {
                throw new ArgumentNullException("receiverEmail is a required property for TransferVouchersRequest and cannot be null");
            }
            this.ReceiverEmail = receiverEmail;
            // to ensure "voucherAmounts" is required (not null)
            if (voucherAmounts == null)
            {
                throw new ArgumentNullException("voucherAmounts is a required property for TransferVouchersRequest and cannot be null");
            }
            this.VoucherAmounts = voucherAmounts;
            // to ensure "voucherIds" is required (not null)
            if (voucherIds == null)
            {
                throw new ArgumentNullException("voucherIds is a required property for TransferVouchersRequest and cannot be null");
            }
            this.VoucherIds = voucherIds;
        }

        /// <summary>
        /// Gets or Sets ReceiverEmail
        /// </summary>
        [DataMember(Name = "receiverEmail", IsRequired = true, EmitDefaultValue = true)]
        public string ReceiverEmail { get; set; }

        /// <summary>
        /// Gets or Sets VoucherAmounts
        /// </summary>
        [DataMember(Name = "voucherAmounts", IsRequired = true, EmitDefaultValue = true)]
        public List<double> VoucherAmounts { get; set; }

        /// <summary>
        /// Gets or Sets VoucherIds
        /// </summary>
        [DataMember(Name = "voucherIds", IsRequired = true, EmitDefaultValue = true)]
        public List<string> VoucherIds { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class TransferVouchersRequest {\n");
            sb.Append("  ReceiverEmail: ").Append(ReceiverEmail).Append("\n");
            sb.Append("  VoucherAmounts: ").Append(VoucherAmounts).Append("\n");
            sb.Append("  VoucherIds: ").Append(VoucherIds).Append("\n");
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
