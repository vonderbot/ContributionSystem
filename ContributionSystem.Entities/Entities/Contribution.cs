namespace ContributionSystem.Entities.Entities
{
    public class Contribution
    {
        public int Id { get; set; }

        public decimal StartValue { get; set; }

        public int Term { get; set; }

        public decimal Percent { get; set; }
    }
}
