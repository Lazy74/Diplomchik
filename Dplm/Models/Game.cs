using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dplm.Models
{
    public class Game
    {
        /// <summary>
        /// ID игры
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название Игры
        /// </summary>
        public string NameGame { get; set; }

        /// <summary>
        /// Массив ID авторов
        /// </summary>
        public int[] IdАuthor { get; set; }

        /// <summary>
        /// Последовательность заданий
        /// </summary>
        public string Sequence { get; set; }

        /// <summary>
        /// Пробег
        /// </summary>
        public int Distance { get; set; }

        /// <summary>
        /// Дата и время начала игры
        /// </summary>
        public DateTime StartGame { get; set; }

        /// <summary>
        /// Информация об игре
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// Количество уровней в игре (ФОРМИРОВАТЬ АВТОМАТИЧЕСКИ ПРИ КАЖДОМ ИЗМЕНЕНИИ ЗАДАНИЙ)
        /// </summary>
        public int AmountLevels { get; set; }

    }
}