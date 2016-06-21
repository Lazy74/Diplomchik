using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            str = str.ToLower();

            StringBuilder sb = new StringBuilder();

            foreach (char c in str)     // удаление всех лишних символов
            {
                if (
                  (c >= '0' && c <= '9') ||
                  //(c >= 'A' && c <= 'Z') ||
                  (c >= 'a' && c <= 'z') ||
                  //(c >= 'А' && c <= 'Я') ||
                  (c >= 'а' && c <= 'я')
                )
                {
                    sb.Append(c);
                }
            }

            str = sb.ToString();

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

            while (!(timeStartLvl < DateTime.Now & timeEndLvl > DateTime.Now) & i < timeout.Count)
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

        /// <summary>
        /// Обновить информацию об ответах
        /// </summary>
        /// <param name="oldAnswers"></param>
        /// <param name="newAnswers"></param>
        /// <returns></returns>
        public static bool UpdateAnswer(List<Answer> oldAnswers, List<Answer> newAnswers, int questId)
        {
            bool Flag = true;

            foreach (Answer oldAnswer in oldAnswers)
            {
                if (!DatabaseND.UpdateAnswers(oldAnswer.Id, oldAnswer.TextAnswer))
                {
                    Flag = false;
                }
            }

            foreach (Answer newAnswer in newAnswers)
            {
                if (!DatabaseND.CreateAnswers(questId, newAnswer.TextAnswer))
                {
                    Flag = false;
                }
            }

            return Flag;
        }
    }
}