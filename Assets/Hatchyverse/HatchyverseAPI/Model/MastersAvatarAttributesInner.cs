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
    /// MastersAvatarAttributesInner
    /// </summary>
    [DataContract(Name = "MastersAvatar_attributes_inner")]
    public partial class MastersAvatarAttributesInner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MastersAvatarAttributesInner" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected MastersAvatarAttributesInner() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="MastersAvatarAttributesInner" /> class.
        /// </summary>
        /// <param name="value">value (required).</param>
        /// <param name="traitType">traitType (required).</param>
        public MastersAvatarAttributesInner(string value = default(string), string traitType = default(string))
        {
            // to ensure "value" is required (not null)
            if (value == null)
            {
                throw new ArgumentNullException("value is a required property for MastersAvatarAttributesInner and cannot be null");
            }
            this.Value = value;
            // to ensure "traitType" is required (not null)
            if (traitType == null)
            {
                throw new ArgumentNullException("traitType is a required property for MastersAvatarAttributesInner and cannot be null");
            }
            this.TraitType = traitType;
        }

        /// <summary>
        /// Gets or Sets Value
        /// </summary>
        [DataMember(Name = "value", IsRequired = true, EmitDefaultValue = true)]
        public string Value { get; set; }

        /// <summary>
        /// Gets or Sets TraitType
        /// </summary>
        [DataMember(Name = "trait_type", IsRequired = true, EmitDefaultValue = true)]
        public string TraitType { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class MastersAvatarAttributesInner {\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("  TraitType: ").Append(TraitType).Append("\n");
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
