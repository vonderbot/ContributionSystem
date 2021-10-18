namespace ContributionSystem.Entities.Entities
{
    public class Contribution
    {
        public decimal StartValue { get; set; }

        public int Term { get; set; }

        public decimal Percent { get; set; }

        public Contribution(decimal newValue, int newTerm, decimal newPercent)
        {
            StartValue = newValue;
            Term = newTerm;
            Percent = newPercent;
        }
    }
}
