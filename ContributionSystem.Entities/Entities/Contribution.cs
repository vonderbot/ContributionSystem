using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ContributionSystem.Entities.Entities
{
    public class Contribution
    {
        [Required]
        [RegularExpression(@"^\-?[0-9]+(?:\.[0-9]{1,2})?$")]
        public decimal StartValue { get; set; }
        [Required]
        public int Term { get; set; }
        [Required]
        [RegularExpression(@"^\-?[0-9]+(?:\.[0-9]{1,2})?$")]
        public decimal Percent { get; set; }

        public Contribution(decimal newValue, int newTerm, decimal newPercent)
        {
            StartValue = newValue;
            Term = newTerm;
            Percent = newPercent;
        }
    }
}
