﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dplm
{
    public class People
    {
        public  int Id { get; set; }

        public string UserLogin
        {
            get { return login; }
            set { login = value.ToLower(); }
        }

        private string login;

        public string UserPass { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string FamiluName { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public string LinkVK { get; set; }

       // Не забыть про аватарку
    }
}