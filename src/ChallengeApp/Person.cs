using System.Collections.Generic;
using System;
using System.IO;

namespace ChallengeApp
{
    public class Person
    {
        public string Forname { get; set; }
        public string Surname { get; set; }
        public Person(string forname, string surname)
        {
            this.Forname = forname;
            this.Surname = surname;
        }
    }
}