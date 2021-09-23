using System;

namespace ContributionSystem.Core.Models.Implementations
{
    public class Contribution
    {
        public decimal StartValue { get; set; }
        //public double CurentValue { get; set; }
        public int Term { get; set; }
        public int Percent { get; set; }
    }
}
