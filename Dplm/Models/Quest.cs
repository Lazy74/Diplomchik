﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dplm.Models
{
    public class Quest
    {
        /// <summary>
        /// ID задания
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Порядковый номер уровня в игре
        /// </summary>
        public int NumberLevel { get; set; }

        /// <summary>
        /// Название уровня
        /// </summary>
        public string NameLevel { get; set; }

        /// <summary>
        /// Коментарий автора
        /// </summary>
        public string AuthorComment { get; set; }

        /// <summary>
        /// Время автоперехода в минутах
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        /// Id игры
        /// </summary>
        public int GameId { get; set; }

        /// <summary>
        /// Текст задания
        /// </summary>
        public string TextQuest { get; set; }

        // TODO Добавить штрафы и вывод информации о них!
    }
}