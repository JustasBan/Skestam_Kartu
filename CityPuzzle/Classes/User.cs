﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;

using BCryptNet = BCrypt.Net.BCrypt;

namespace CityPuzzle.Classes
{
    public class User :IEquatable<User>
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }

        public List<string> QuestsCompleted = new List<string>();

        public double maxQuestDistance { get; set; }

        public User() { }
        public static Boolean CheckPassword(string name, string pass)
        {
             if (String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(pass))
            {
                return false;
            }

           // pass = PassToHash(pass);//     //                              //encrypt
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<User>();
                //conn.DeleteAll<User>();
                var info = conn.Table<User>().ToList();

                App.CurrentUser = info.SingleOrDefault(x => x.UserName.ToLower().Equals(name.ToLower()) && PassVerification(pass,x.Pass));
            };
            return App.CurrentUser != null;
        }
        public static Boolean CheckUser(string name)
        { 
            using (SQLiteConnection conn = new SQLiteConnection(App.FilePath))
            {
                conn.CreateTable<User>();

                var info = conn.Table<User>().ToList();
                Console.WriteLine(info.Count);
                foreach (User n in info)
                {
                    if (n.UserName.ToLower().Equals(name.ToLower()))
                    {
                        return false;
                    }
                }
            };
            return true;
        }

        public static string PassToHash(string pass)
        {
            string passwordHash = BCryptNet.HashPassword(pass);
            return passwordHash;
        }

        public static bool PassVerification(string pass, string passwordHash)
        {
            bool verified = BCryptNet.Verify(pass, passwordHash);
            return verified;
        }

        public bool Equals(User other)
        {
            if (this == null || other == null)
                return false;
            else if (other.QuestsCompleted.Count == this.QuestsCompleted.Count)
                return true;
            else
                return false;
        }
    }
}
