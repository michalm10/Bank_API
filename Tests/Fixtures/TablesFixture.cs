using nbp_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Fixtures
{
    internal static class TablesFixture
    {
        public static ExchangesAModel GetAOne() =>
            new ExchangesAModel()
            {
                table = "C",
                currency = "asdf",
                code = "GBP",
                rates = new List<RatesModel> {
                    new RatesModel()
                    {
                        no = "asdf",
                        effectiveDate = "2022-04-12",
                        mid = 5.5
                    }
                }
            };
        public static ExchangesAModel GetAMultiple() =>
            new ExchangesAModel()
            {
                table = "C",
                currency = "asdf",
                code = "GBP",
                rates = new List<RatesModel> {
                    new RatesModel()
                    {
                        no = "asdf",
                        effectiveDate = "2022-04-12",
                        mid = 9
                    },
                    new RatesModel()
                    {
                        no = "asdf",
                        effectiveDate = "2022-04-13",
                        mid = 7
                    },
                    new RatesModel()
                    {
                        no = "asdf",
                        effectiveDate = "2022-04-14",
                        mid = 5.5
                    },
                    new RatesModel()
                    {
                        no = "asdf",
                        effectiveDate = "2022-04-15",
                        mid = 10
                    },
                    new RatesModel()
                    {
                        no = "asdf",
                        effectiveDate = "2022-04-16",
                        mid = 11
                    },
                    new RatesModel()
                    {
                        no = "asdf",
                        effectiveDate = "2022-04-17",
                        mid = 8
                    },
                }
            };
        public static ExchangesCModel GetCMultiple() =>
            new ExchangesCModel()
            {
                table = "C",
                currency = "asdf",
                code = "GBP",
                rates = new List<BuyAskModel> {
                    new BuyAskModel()
                    {
                        no = "asdf",
                        effectiveDate = "2022-04-12",
                        bid = 9.5,
                        ask = 8.23
                    },
                    new BuyAskModel()
                    {
                        no = "asdf",
                        effectiveDate = "2022-04-13",
                        bid = 9.55,
                        ask = 8.23
                    },
                    new BuyAskModel()
                    {
                        no = "asdf",
                        effectiveDate = "2022-04-14",
                        bid = 9.5,
                        ask = 8.5
                    },
                    new BuyAskModel()
                    {
                        no = "asdf",
                        effectiveDate = "2022-04-15",
                        bid = 9.5,
                        ask = 4.23
                    },
                    new BuyAskModel()
                    {
                        no = "asdf",
                        effectiveDate = "2022-04-16",
                        bid = 9.56,
                        ask = 8.23
                    },
                    new BuyAskModel()
                    {
                        no = "asdf",
                        effectiveDate = "2022-04-17",
                        bid = 9.5,
                        ask = 6.23
                    }
                }
            };
    }
}
