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
    /// MastersColor
    /// </summary>
    [DataContract(Name = "MastersColor")]
    public partial class MastersColor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MastersColor" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected MastersColor() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="MastersColor" /> class.
        /// </summary>
        /// <param name="id">id (required).</param>
        /// <param name="color">color (required).</param>
        /// <param name="typeId">typeId (required).</param>
        /// <param name="name">name (required).</param>
        public MastersColor(double id = default(double), string color = default(string), double typeId = default(double), string name = default(string))
        {
            this.Id = id;
            // to ensure "color" is required (not null)
            if (color == null)
            {
                throw new ArgumentNullException("color is a required property for MastersColor and cannot be null");
            }
            this.Color = color;
            this.TypeId = typeId;
            // to ensure "name" is required (not null)
            if (name == null)
            {
                throw new ArgumentNullException("name is a required property for MastersColor and cannot be null");
            }
            this.Name = name;
        }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", IsRequired = true, EmitDefaultValue = true)]
        public double Id { get; set; }

        /// <summary>
        /// Gets or Sets Color
        /// </summary>
        [DataMember(Name = "color", IsRequired = true, EmitDefaultValue = true)]
        public string Color { get; set; }

        /// <summary>
        /// Gets or Sets TypeId
        /// </summary>
        [DataMember(Name = "typeId", IsRequired = true, EmitDefaultValue = true)]
        public double TypeId { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = true)]
        public string Name { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class MastersColor {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Color: ").Append(Color).Append("\n");
            sb.Append("  TypeId: ").Append(TypeId).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
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
