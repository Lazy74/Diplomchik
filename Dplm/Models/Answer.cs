using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dplm.Models
{
    public class Answer
    {
        public int Id { get; set; }

        public int QuestId { get; set; }

        public string TextAnswer { get; set; }
    }
}