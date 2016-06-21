using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dplm.Models
{
    public class TeamPlay
    {
        /// <summary>
        /// ID записи
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id игры
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Id команды
        /// </summary>
        public int TeamId { get; set; }

        /// <summary>
        /// Участие в игре
        /// </summary>
        public bool Access { get; set; }
    }
}