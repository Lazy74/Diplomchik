using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dplm.Models
{
    public class GameEngine
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
        public static int CorrectionLevel(Game game)
        {
            int i = 0;

            DateTime timeStartLvl;
            DateTime timeEndLvl;

            // timeLvl = DatabaseND.GetQuest(game.Id, lvl).TimeOut;    // Получаем время на уровнe. Здесь к времени старта прибавляем время таймаута N задания 
            List<int> timeout = DatabaseND.GetTimeoutAllQuest(game.Id);

            timeStartLvl = game.StartGame;
            timeEndLvl = timeStartLvl.AddMinutes(timeout[i]);
            i++;

            while (!(timeStartLvl< DateTime.Now & timeEndLvl>DateTime.Now) & i < timeout.Count)
            {
                timeStartLvl = timeEndLvl;
                timeEndLvl = timeStartLvl.AddMinutes(timeout[i]);
                i++;
            }

            if (timeEndLvl > DateTime.Now)
            {
                return i;
            }
            else
            {
                return -1;
            }
        }
    }
}