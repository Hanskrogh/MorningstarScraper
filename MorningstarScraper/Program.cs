using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace MorningstarScraper
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-us");

            List<string> urlArray = new List<string>();
            urlArray.Add("https://www.morningstar.dk/dk/funds/snapshot/snapshot.aspx?id=F00000XLK3&tab=3");
            urlArray.Add("https://www.morningstar.dk/dk/funds/snapshot/snapshot.aspx?id=F0GBR04L59&tab=3");
#pragma warning disable CS0162 // Unreachable code detected
            for (int j = 0; j < urlArray.Count; j++)
#pragma warning restore CS0162 // Unreachable code detected
            {
                using (var wc = new WebClient())
                {
                    //Scraper Sparinvest INDEX Mellem Risiko KL | SPVIMRKL:

                    var htmlData = wc.DownloadData(urlArray[j]);
                    var html = Encoding.UTF8.GetString(htmlData);

                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(html);

                    HtmlNode[] elements = (HtmlNode[])htmlDoc.DocumentNode.CssSelect("#portfolioAssetAllocationDiv .portfolioAssetAllocationTable tr .label");

                    AssetAllocation assetAllocation = new AssetAllocation();


                    foreach (var element in elements)
                    {
                        var container = element.ParentNode;
                        var containerValues = container.CssSelect("td.value").ToArray();

                        var longVal = float.Parse(containerValues[0].InnerHtml.Replace(",", "."));
                        var shortVal = float.Parse(containerValues[1].InnerHtml.Replace(",", "."));
                        var nettoVal = float.Parse(containerValues[2].InnerHtml.Replace(",", "."));

                        switch (element.InnerHtml)
                        {
                            case "Aktier":
                                assetAllocation.StockLong = longVal;
                                assetAllocation.StockShort = shortVal;
                                assetAllocation.StockNetto = nettoVal;
                                break;

                            case "Obligationer":
                                assetAllocation.ObligationLong = longVal;
                                assetAllocation.ObligationShort = shortVal;
                                assetAllocation.ObligationNetto = nettoVal;
                                break;


                            case "Kontant o. lign.":
                                assetAllocation.CashLong = longVal;
                                assetAllocation.CashShort = shortVal;
                                assetAllocation.CashNetto = nettoVal;
                                break;

                            case "Andet":
                                assetAllocation.OtherLong = longVal;
                                assetAllocation.OtherShort = shortVal;
                                assetAllocation.OtherNetto = nettoVal;
                                break;

                            default: break;
                        }
                    }

                    elements = (HtmlNode[])htmlDoc.DocumentNode.CssSelect(".portfolioBondSummaryMaturityDistributionTable tr td.heading");

                    var runningTime = elements.FirstOrDefault(c => c.InnerHtml.Equals("Løbetid - eksponering", StringComparison.CurrentCultureIgnoreCase)).ParentNode.ParentNode
                        .CssSelect("tr").Skip(2);

                    var obligations = new List<RunTimeObligation>();

                    foreach (var runningTimeElement in runningTime)
                    {
                        var label = runningTimeElement.CssSelect(".label").FirstOrDefault()?.InnerHtml;
                        var number = float.Parse(runningTimeElement.CssSelect(".value").FirstOrDefault()?.InnerHtml.Replace(",", "."));

                        obligations.Add(new RunTimeObligation
                        {
                            ExposeTitle = label,
                            Percentage = number
                        });
                    }

                    Console.WriteLine(assetAllocation.ToString());
                    foreach (var obligation in obligations)
                    {
                        Console.WriteLine(obligation.ToString());
                    }

                    while (true) { }
                }
            }
        }
    }
}
