using Leaf.xNet;
using Newtonsoft.Json;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace OpenCCTV.CCTV
{
    internal class CountrySearcher
    {
        /// <summary>
        /// Displays the amount of cameras per country.
        /// </summary>
        public static void DisplayCountryInfo()
        {
            HttpRequest hr = new HttpRequest()
            {
                IgnoreProtocolErrors = true,
            };

            hr.SslCertificateValidatorCallback += (obj, cert, ssl, error) => (cert as X509Certificate2).Verify();

            try
            {
                HttpResponse countryResp = hr.Get("http://www.insecam.org/en/jsoncountries/");
                string countryJson = countryResp.ToString();
                var countryData = JsonConvert.DeserializeObject<Utils.Country>(countryJson);

                if (countryData != null && countryData.Countries != null)
                {
                    foreach (var country in countryData.Countries)
                    {
                        if (country.Value.Country == "-") continue;

                        Interface.InterfaceMaker.PrintCountryInfo(
                            country.Key.PadRight(2),
                            country.Value.Country.PadRight(25),
                            country.Value.Count);
                    }
                }

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Interface.InterfaceMaker.WriteLine(Color.White, "Error", "Couldn't obtain Country Statistics.\n");
            }
        }
    }
}