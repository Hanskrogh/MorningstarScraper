using System;
using System.Collections.Generic;
using System.Text;

namespace MorningstarScraper
{
    public class RunTimeObligation
    {
        public string ExposeTitle { get; set; }
        public float Percentage { get; set; }

        public override string ToString()
        {
            return $"{ExposeTitle} - Procent: {Percentage} %";
        }
    }
}
