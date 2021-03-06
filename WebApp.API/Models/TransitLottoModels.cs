﻿using Framework.Data;
using Newtonsoft.Json;
using System;
using WebApp.API.Contracts;

namespace WebApp.API.Models
{
    /// <summary>
    /// Transit draw model based on event
    /// </summary>
    /// <seealso cref="Framework.Data.Entity" />
    /// <seealso cref="WebApp.API.Contracts.ITransitLottoDrawEvent" />
    public class TransitLottoDrawModel : Entity
    {
        /// <summary>
        /// The draw winning numbers
        /// </summary>
        [JsonProperty("draw_winning_numbers")]
        public int[] DrawWinningNumbers { get; set; }

        /// <summary>
        /// The draw date time
        /// </summary>
        [JsonProperty("draw_date_time")]
        public DateTime DrawDateTime { get; set; }

        /// <summary>
        /// The draw number
        /// </summary>
        [JsonProperty("draw_number")]
        public int DrawNumber { get; set; }

        /// <summary>
        /// The draw status
        /// </summary>
        [JsonProperty("draw_status")]
        public DrawStatusCode DrawStatus { get; set; }

    }
}