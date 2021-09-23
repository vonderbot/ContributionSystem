using System;

namespace ContributionSystem.Entities.Entities
{
    public class Contribution
    {
        public decimal StartValue { get; set; }
        public int Term { get; set; }
        public int Percent { get; set; }

        public Contribution(decimal newValue, int newTerm, int newPercent)
        {
            newValue = Math.Round(newValue, 2);
            StartValue = newValue;
            Term = newTerm;
            Percent = newPercent;
        }
    }
}
