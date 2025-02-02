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
    /// MastersAvatar
    /// </summary>
    [DataContract(Name = "MastersAvatar")]
    public partial class MastersAvatar
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MastersAvatar" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected MastersAvatar() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="MastersAvatar" /> class.
        /// </summary>
        /// <param name="tokenId">tokenId (required).</param>
        /// <param name="image">image (required).</param>
        /// <param name="name">name (required).</param>
        /// <param name="attributes">attributes (required).</param>
        /// <param name="equippedItems">equippedItems (required).</param>
        /// <param name="traits">traits (required).</param>
        /// <param name="layers">layers (required).</param>
        public MastersAvatar(double tokenId = default(double), string image = default(string), string name = default(string), List<MastersAvatarAttributesInner> attributes = default(List<MastersAvatarAttributesInner>), List<MastersItem> equippedItems = default(List<MastersItem>), List<MastersTrait> traits = default(List<MastersTrait>), List<string> layers = default(List<string>))
        {
            this.TokenId = tokenId;
            // to ensure "image" is required (not null)
            if (image == null)
            {
                throw new ArgumentNullException("image is a required property for MastersAvatar and cannot be null");
            }
            this.Image = image;
            // to ensure "name" is required (not null)
            if (name == null)
            {
                throw new ArgumentNullException("name is a required property for MastersAvatar and cannot be null");
            }
            this.Name = name;
            // to ensure "attributes" is required (not null)
            if (attributes == null)
            {
                throw new ArgumentNullException("attributes is a required property for MastersAvatar and cannot be null");
            }
            this.Attributes = attributes;
            // to ensure "equippedItems" is required (not null)
            if (equippedItems == null)
            {
                throw new ArgumentNullException("equippedItems is a required property for MastersAvatar and cannot be null");
            }
            this.EquippedItems = equippedItems;
            // to ensure "traits" is required (not null)
            if (traits == null)
            {
                throw new ArgumentNullException("traits is a required property for MastersAvatar and cannot be null");
            }
            this.Traits = traits;
            // to ensure "layers" is required (not null)
            if (layers == null)
            {
                throw new ArgumentNullException("layers is a required property for MastersAvatar and cannot be null");
            }
            this.Layers = layers;
        }

        /// <summary>
        /// Gets or Sets TokenId
        /// </summary>
        [DataMember(Name = "tokenId", IsRequired = true, EmitDefaultValue = true)]
        public double TokenId { get; set; }

        /// <summary>
        /// Gets or Sets Image
        /// </summary>
        [DataMember(Name = "image", IsRequired = true, EmitDefaultValue = true)]
        public string Image { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = true)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Attributes
        /// </summary>
        [DataMember(Name = "attributes", IsRequired = true, EmitDefaultValue = true)]
        public List<MastersAvatarAttributesInner> Attributes { get; set; }

        /// <summary>
        /// Gets or Sets EquippedItems
        /// </summary>
        [DataMember(Name = "equippedItems", IsRequired = true, EmitDefaultValue = true)]
        public List<MastersItem> EquippedItems { get; set; }

        /// <summary>
        /// Gets or Sets Traits
        /// </summary>
        [DataMember(Name = "traits", IsRequired = true, EmitDefaultValue = true)]
        public List<MastersTrait> Traits { get; set; }

        /// <summary>
        /// Gets or Sets Layers
        /// </summary>
        [DataMember(Name = "layers", IsRequired = true, EmitDefaultValue = true)]
        public List<string> Layers { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class MastersAvatar {\n");
            sb.Append("  TokenId: ").Append(TokenId).Append("\n");
            sb.Append("  Image: ").Append(Image).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Attributes: ").Append(Attributes).Append("\n");
            sb.Append("  EquippedItems: ").Append(EquippedItems).Append("\n");
            sb.Append("  Traits: ").Append(Traits).Append("\n");
            sb.Append("  Layers: ").Append(Layers).Append("\n");
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
