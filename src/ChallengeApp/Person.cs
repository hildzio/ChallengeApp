using System.Collections.Generic;
using System;
using System.IO;

namespace ChallengeApp
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Person(string name, string surname)
        {
            this.Name = name;
            this.Surname = surname;
        }
    }
}