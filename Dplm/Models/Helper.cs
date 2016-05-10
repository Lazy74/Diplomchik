﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dplm.Models
{
    public class Helper
    {
        /// <summary>
        /// Удаление не нужных символов из ответа. Пока удаляет только пробелы в начале и в конце строки
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveExtraCharacters(string str)
        {
            str = str.Trim();   // Убираем все пробелы в начале и в конце слова
            return str;
        }

        /// <summary>
        /// Проверить не было ли автопереходов, если да то выдать новый уровень
        /// </summary>
        /// <param name="game">Модель игры</param>
        /// <returns></returns>
        public static LvlAndTime CorrectionLevel(Game game, LvlAndTime lvlAndTime)
        {
            int i = 0;

            DateTime timeStartLvl;
            DateTime timeEndLvl;

            // timeLvl = DatabaseND.GetQuest(game.Id, lvl).TimeOut;    // Получаем время на уровнe. Здесь к времени старта прибавляем время таймаута N задания 
            List<int> timeout = DatabaseND.GetTimeoutAllQuest(game.Id);

            if (lvlAndTime.numburLVL == 1)
            {
                // Если команда на 1 уровне, старт берем от начала игры
                timeStartLvl = game.StartGame;
                timeEndLvl = timeStartLvl.AddMinutes(timeout[i]);
            }
            else
            {
                // Иначе начало берем от n уровня и i приравниваем уровню
                i = lvlAndTime.numburLVL - 1;
                timeStartLvl = lvlAndTime.StartLVL;
                timeEndLvl = timeStartLvl.AddMinutes(timeout[i]);
            }


            i++;

            while (!(timeStartLvl< DateTime.Now & timeEndLvl>DateTime.Now) & i < timeout.Count)
            {
                timeStartLvl = timeEndLvl;
                timeEndLvl = timeStartLvl.AddMinutes(timeout[i]);
                i++;
            }

            lvlAndTime.numburLVL = i;
            lvlAndTime.StartLVL = timeStartLvl;
            lvlAndTime.EndLVL = timeEndLvl;

            //if (timeEndLvl > DateTime.Now)
            //{
            //    //return i;

            //}
            //else
            //{
            //    //return -1;
            //}

            return lvlAndTime;
        }


        /// <summary>
        /// Получить количество секунд до ногово уровня
        /// </summary>
        /// <param name="EndLVL">Время окончания уровня</param>
        /// <returns></returns>
        public static int GetTimeTransition(DateTime EndLVL)
        {
            // Difference in days, hours, and minutes.
            TimeSpan ts = EndLVL - DateTime.Now;

            return ts.Seconds + ts.Minutes * 60 + ts.Hours * 60 * 60;
        }

        //public static List<string> ConvertGameInListName(List<Game> games)
        //{
        //    List<string> fooList = new List<string>();

        //    foreach (Game game in games)
        //    {
                
        //    }
        //}
    }
}