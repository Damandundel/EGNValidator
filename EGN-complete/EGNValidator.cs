using System;

namespace EGNValidatorApp
{
    public class EGNValidator : IValidator
    {
        private static readonly string[] Regions = {
            "Blagoevgrad", "Burgas", "Varna", "Veliko Tarnovo", "Vidin", "Vratsa",
            "Gabrovo", "Kardzhali", "Kyustendil", "Lovech", "Montana", "Pazardzhik",
            "Pernik", "Pleven", "Plovdiv", "Razgrad", "Ruse", "Silistra", "Sliven",
            "Smolyan", "Sofia City", "Sofia Province", "Stara Zagora", "Dobrich",
            "Targovishte", "Haskovo", "Shumen", "Yambol", "Other"
        };

        public bool Validate(string egn)
        {
            if (egn.Length != 10 || !long.TryParse(egn, out _))
                return false;

            int year = int.Parse(egn.Substring(0, 2));
            int month = int.Parse(egn.Substring(2, 2));
            int day = int.Parse(egn.Substring(4, 2));

            if (month >= 1 && month <= 12)
                year += 1900;
            else if (month >= 21 && month <= 32)
            {
                year += 1800;
                month -= 20;
            }
            else if (month >= 41 && month <= 52)
            {
                year += 2000;
                month -= 40;
            }
            else
                return false;

            try
            {
                DateTime dob = new DateTime(year, month, day);
            }
            catch
            {
                return false;
            }

            int[] weights = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
            int checksum = 0;
            for (int i = 0; i < 9; i++)
                checksum += weights[i] * (egn[i] - '0');

            int remainder = checksum % 11;
            if (remainder == 10) remainder = 0;

            return remainder == (egn[9] - '0');
        }

        public Person ExtractPersonInfo(string egn, string name = "Unknown")
        {
            if (!Validate(egn))
                return null;

            int year = int.Parse(egn.Substring(0, 2));
            int month = int.Parse(egn.Substring(2, 2));
            int day = int.Parse(egn.Substring(4, 2));

            if (month >= 1 && month <= 12)
                year += 1900;
            else if (month >= 21 && month <= 32)
            {
                year += 1800;
                month -= 20;
            }
            else
            {
                year += 2000;
                month -= 40;
            }

            DateTime dob = new DateTime(year, month, day);
            int regionCode = int.Parse(egn.Substring(6, 3));
            string region = Regions[Math.Min(regionCode / 30, Regions.Length - 1)];
            string gender = (regionCode % 2 == 0) ? "Male" : "Female";

            return new Person
            {
                Name = name,
                EGN = egn,
                DateOfBirth = dob,
                Gender = gender,
                Region = region
            };
        }
    }
}