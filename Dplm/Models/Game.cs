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
        /// Переделать в массив ID авторов, а пока щдесь будет 1 автор!
        /// </summary>
        public int IdАuthor { get; set; }
        //public int[] IdАuthor { get; set; }

        /// <summary>
        /// Последовательность заданий (линейная, не линейная, указанная)
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
        /// Дата и время конца игры
        /// </summary>
        public DateTime EndGame { get; set; }

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