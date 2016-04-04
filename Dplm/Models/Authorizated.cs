using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dplm.Models
{
    public class Authorizated
    {
        public static Dictionary<string, People> Data = new Dictionary<string, People>();

        public static string Auth(People people)
        {
            string guid = Guid.NewGuid().ToString("N");
            Authorizated.Data.Add(guid, people);

            return guid;
        }

        public static People AuthorizationCheck(string guide)
        {
            People people = new People();
            if (Data.TryGetValue(guide, out people))
            {
                return people;
            }
            return null;
        }

        public static bool LogOut(string guid)
        {
            Data.Remove(guid);
            if (!Data.ContainsKey(guid))
            {
                return true;
            }
            return false;
        }
    }
}