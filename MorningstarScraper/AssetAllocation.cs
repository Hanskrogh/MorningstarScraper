using System;
using System.Collections.Generic;
using System.Text;

namespace MorningstarScraper
{
    public class AssetAllocation
    {
        public float StockLong { get; set; }
        public float StockShort { get; set; }
        public float StockNetto { get; set; }

        public float ObligationLong { get; set; }
        public float ObligationShort { get; set; }
        public float ObligationNetto { get; set; }

        public float CashLong { get; set; }
        public float CashShort { get; set; }
        public float CashNetto { get; set; }

        public float OtherLong { get; set; }
        public float OtherShort { get; set; }
        public float OtherNetto { get; set; }


        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("Aktier");
            builder.AppendLine($"Lang: {StockLong}");
            builder.AppendLine($"Kort: {StockShort}");
            builder.AppendLine($"Netto: {StockNetto}");
            builder.AppendLine("------------");
            builder.AppendLine("Obligationer");
            builder.AppendLine($"Lang: {ObligationLong}");
            builder.AppendLine($"Kort: {ObligationShort}");
            builder.AppendLine($"Netto: {ObligationNetto}");
            builder.AppendLine("------------");
            builder.AppendLine("Kontant o. lign.");
            builder.AppendLine($"Lang: {CashLong}");
            builder.AppendLine($"Kort: {CashShort}");
            builder.AppendLine($"Netto: {CashNetto}");
            builder.AppendLine("------------");
            builder.AppendLine("Andet");
            builder.AppendLine($"Lang: {OtherLong}");
            builder.AppendLine($"Kort: {OtherShort}");
            builder.AppendLine($"Netto: {OtherNetto}");
            builder.AppendLine("------------");
            return builder.ToString();
        }
    }
}
