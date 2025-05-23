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
    /// MastersLootbox
    /// </summary>
    [DataContract(Name = "MastersLootbox")]
    public partial class MastersLootbox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MastersLootbox" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected MastersLootbox() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="MastersLootbox" /> class.
        /// </summary>
        /// <param name="id">id (required).</param>
        /// <param name="name">name (required).</param>
        /// <param name="active">active (required).</param>
        /// <param name="image">image (required).</param>
        /// <param name="order">order (required).</param>
        /// <param name="chainId">chainId (required).</param>
        /// <param name="gameId">gameId (required).</param>
        /// <param name="genderId">genderId (required).</param>
        /// <param name="ticketId">ticketId (required).</param>
        /// <param name="itemWeights">itemWeights (required).</param>
        /// <param name="prices">prices (required).</param>
        public MastersLootbox(double id = default(double), string name = default(string), bool active = default(bool), string image = default(string), double order = default(double), double? chainId = default(double?), string gameId = default(string), double genderId = default(double), double? ticketId = default(double?), List<MastersLootboxItem> itemWeights = default(List<MastersLootboxItem>), List<LootboxPrice> prices = default(List<LootboxPrice>))
        {
            this.Id = id;
            // to ensure "name" is required (not null)
            if (name == null)
            {
                throw new ArgumentNullException("name is a required property for MastersLootbox and cannot be null");
            }
            this.Name = name;
            this.Active = active;
            // to ensure "image" is required (not null)
            if (image == null)
            {
                throw new ArgumentNullException("image is a required property for MastersLootbox and cannot be null");
            }
            this.Image = image;
            this.Order = order;
            // to ensure "chainId" is required (not null)
            if (chainId == null)
            {
                throw new ArgumentNullException("chainId is a required property for MastersLootbox and cannot be null");
            }
            this.ChainId = chainId;
            // to ensure "gameId" is required (not null)
            if (gameId == null)
            {
                throw new ArgumentNullException("gameId is a required property for MastersLootbox and cannot be null");
            }
            this.GameId = gameId;
            this.GenderId = genderId;
            // to ensure "ticketId" is required (not null)
            if (ticketId == null)
            {
                throw new ArgumentNullException("ticketId is a required property for MastersLootbox and cannot be null");
            }
            this.TicketId = ticketId;
            // to ensure "itemWeights" is required (not null)
            if (itemWeights == null)
            {
                throw new ArgumentNullException("itemWeights is a required property for MastersLootbox and cannot be null");
            }
            this.ItemWeights = itemWeights;
            // to ensure "prices" is required (not null)
            if (prices == null)
            {
                throw new ArgumentNullException("prices is a required property for MastersLootbox and cannot be null");
            }
            this.Prices = prices;
        }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", IsRequired = true, EmitDefaultValue = true)]
        public double Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = true)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Active
        /// </summary>
        [DataMember(Name = "active", IsRequired = true, EmitDefaultValue = true)]
        public bool Active { get; set; }

        /// <summary>
        /// Gets or Sets Image
        /// </summary>
        [DataMember(Name = "image", IsRequired = true, EmitDefaultValue = true)]
        public string Image { get; set; }

        /// <summary>
        /// Gets or Sets Order
        /// </summary>
        [DataMember(Name = "order", IsRequired = true, EmitDefaultValue = true)]
        public double Order { get; set; }

        /// <summary>
        /// Gets or Sets ChainId
        /// </summary>
        [DataMember(Name = "chainId", IsRequired = true, EmitDefaultValue = true)]
        public double? ChainId { get; set; }

        /// <summary>
        /// Gets or Sets GameId
        /// </summary>
        [DataMember(Name = "gameId", IsRequired = true, EmitDefaultValue = true)]
        public string GameId { get; set; }

        /// <summary>
        /// Gets or Sets GenderId
        /// </summary>
        [DataMember(Name = "genderId", IsRequired = true, EmitDefaultValue = true)]
        public double GenderId { get; set; }

        /// <summary>
        /// Gets or Sets TicketId
        /// </summary>
        [DataMember(Name = "ticketId", IsRequired = true, EmitDefaultValue = true)]
        public double? TicketId { get; set; }

        /// <summary>
        /// Gets or Sets ItemWeights
        /// </summary>
        [DataMember(Name = "itemWeights", IsRequired = true, EmitDefaultValue = true)]
        public List<MastersLootboxItem> ItemWeights { get; set; }

        /// <summary>
        /// Gets or Sets Prices
        /// </summary>
        [DataMember(Name = "prices", IsRequired = true, EmitDefaultValue = true)]
        public List<LootboxPrice> Prices { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class MastersLootbox {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Active: ").Append(Active).Append("\n");
            sb.Append("  Image: ").Append(Image).Append("\n");
            sb.Append("  Order: ").Append(Order).Append("\n");
            sb.Append("  ChainId: ").Append(ChainId).Append("\n");
            sb.Append("  GameId: ").Append(GameId).Append("\n");
            sb.Append("  GenderId: ").Append(GenderId).Append("\n");
            sb.Append("  TicketId: ").Append(TicketId).Append("\n");
            sb.Append("  ItemWeights: ").Append(ItemWeights).Append("\n");
            sb.Append("  Prices: ").Append(Prices).Append("\n");
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
