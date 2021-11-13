﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CityPuzzle.Classes
{
    class RegexDelegate
    {
        public static bool validUsername(string text)
        {
            string x = "^(?=.{6,12}$)(?![_.])(?!.*[_.]{2})[a-z0-9._]+(?<![_.])$";
            Regex reg =  new Regex(x, RegexOptions.IgnoreCase);
            return reg.IsMatch(text);
        }

        public static bool validPassword(string text)
        {
            string x = "(?!^[0-9]*$)(?!^[a-z]*$)^([a-z0-9]{8,15})$";
            Regex reg = new Regex(x, RegexOptions.IgnoreCase);
            return reg.IsMatch(text);
        }

        public static bool validEmail(string text)
        {
            string x = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            Regex reg = new Regex(x, RegexOptions.IgnoreCase);
            return reg.IsMatch(text);
        }
    }
}
