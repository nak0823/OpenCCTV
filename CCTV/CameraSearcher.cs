using Leaf.xNet;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace OpenCCTV.CCTV
{
    internal class CameraSearcher
    {
        /// <summary>
        /// Finds Cameras with ease.
        /// </summary>
        /// <param name="countryCode">The country code</param>
        public static void FindCameras(string countryCode)
        {
            Console.Clear();
            Console.Title = $"CCTV Browser by Serialized - Searching Cameras in {countryCode}";
            Interface.InterfaceMaker.PrintLogo();

            List<string> camerasList = new List<string>();

            HttpRequest hr = new HttpRequest()
            {
                IgnoreProtocolErrors = true,
            };

            hr.SslCertificateValidatorCallback += (obj, cert, ssl, error) => (cert as X509Certificate2).Verify();

            try
            {
                int lastPage = GetLastPage(hr, countryCode);

                for (int i = 0; i < lastPage; i++)
                {
                    string[] cameras = GetCameras(hr, countryCode, i);

                    for (int j = 0; j < cameras.Length; j++)
                    {
                        camerasList.Add(cameras[j]);
                        Interface.InterfaceMaker.WriteLine(Color.White, $"{countryCode}-{camerasList.Count}", $"{cameras[j]}\n");
                    }
                }

                Console.WriteLine();
                Interface.InterfaceMaker.WriteLine(Color.White, "A", $"Export All Cameras\n");
                Interface.InterfaceMaker.WriteLine(Color.White, "B", $"Open All Cameras\n");
                Interface.InterfaceMaker.WriteLine(Color.White, "C", $"Back\n");
                Interface.InterfaceMaker.WriteLine(Color.White, ">", $"");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "A":
                        File.WriteAllLines($"{countryCode}.txt", camerasList);
                        Interface.InterfaceMaker.WriteLine(Color.White, "~", "Exported All Cameras!\n");
                        Utils.CountryMenu();
                        break;

                    case "B":
                        foreach (string cameras in camerasList)
                            Program.OpenWebpage(cameras);

                        break;

                    case "C":
                        Utils.CountryMenu();
                        break;
                }
            }
            catch (Exception ex)
            {
                Interface.InterfaceMaker.WriteLine(Color.White, "Error", "Couldn't Obtain Cameras.\n");
            }
        }

        /// <summary>
        /// Checks how many pages there are to check
        /// </summary>
        /// <param name="hr">The HttpRequest</param>
        /// <param name="country">The country</param>
        /// <returns>The number of pages</returns>
        private static int GetLastPage(HttpRequest hr, string country)
        {
            var camResp = hr.Get($"http://www.insecam.org/en/bycountry/{country}");
            var lastPageMatch = Regex.Match(camResp.ToString(), @"pagenavigator\(""\?page="", (\d+)");
            return lastPageMatch.Success ? int.Parse(lastPageMatch.Groups[1].Value) : 0;
        }

        /// <summary>
        /// Gets the list of open cameras.
        /// </summary>
        /// <param name="hr"></param>
        /// <param name="country"></param>
        /// <param name="page"></param>
        /// <returns>A list of cameras</returns>
        private static string[] GetCameras(HttpRequest hr, string country, int page)
        {
            var camResp = hr.Get($"http://www.insecam.org/en/bycountry/{country}/?page={page}");
            return Regex.Matches(camResp.ToString(), @"http://\d+.\d+.\d+.\d+:\d+")
                        .Cast<Match>()
                        .Select(match => match.Value)
                        .ToArray();
        }
    }
}