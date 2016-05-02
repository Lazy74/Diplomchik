using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dplm.Models
{
    public class Team
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int IdCommander { get; set; }
    }
}