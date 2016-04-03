using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dplm
{
    public class People
    {
        public  int Id { get; set; }

        public string UserLogin { get; set; }

        public string UserPass { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string FamiluName { get; set; }

        public string Name { get; set; }

        public string Birthday { get; set; }

        public string LinkVK { get; set; }

       // Не забыть про аватарку
    }
}