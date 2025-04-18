using System;

namespace EGNValidatorApp
{
    public class Person
    {
        public string Name { get; set; }
        public string EGN { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Region { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\nEGN: {EGN}\nDate of Birth: {DateOfBirth:yyyy-MM-dd}\nGender: {Gender}\nRegion: {Region}";
        }
    }
}