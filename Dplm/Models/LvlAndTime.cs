using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dplm.Models
{
    public class LvlAndTime
    {
        /// <summary>
        /// Номер уровня
        /// </summary>
        public int numburLVL { get; set; }

        /// <summary>
        /// Время начала уровня
        /// </summary>
        public DateTime StartLVL { get; set; }

        /// <summary>
        /// Время конца уровня
        /// </summary>
        public DateTime EndLVL { get; set; }
    }
}