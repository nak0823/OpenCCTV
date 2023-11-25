using System.Drawing;

namespace OpenCCTV.CCTV
{
    /// <summary>
    /// Utility class containing various visible output methods.
    /// </summary>
    internal class Utils
    {
        /// <summary>
        /// Menu that helps displaying country statistics and user input.
        /// </summary>
        public static void CountryMenu()
        {
            SetConsoleTitle("CCTV Browser by Serialized - Displaying Information By Country");
            Interface.InterfaceMaker.WriteLine(Color.White, "~", "Retrieving Country Statistics\n\n");

            /* Display the fetched information. */
            CountrySearcher.DisplayCountryInfo();

            Interface.InterfaceMaker.WriteLine(Color.White, "~", "Enter Your Wanted Country\n");
            Interface.InterfaceMaker.WriteLine(Color.White, ">", "");

            Console.ForegroundColor = ConsoleColor.White;
            string countryInput = Console.ReadLine();
            CameraSearcher.FindCameras(countryInput);
        }

        /// <summary>
        /// Set Console.Title and Flush Console.
        /// </summary>
        /// <param name="title">The consoles title.</param>
        private static void SetConsoleTitle(string title)
        {
            Console.Clear();
            Console.Title = title;
            Interface.InterfaceMaker.PrintLogo();
        }

        /// <summary>
        /// Deserializable class holding Country Information
        /// </summary>
        public class Country
        {
            public string? Status { get; set; }
            public Dictionary<string, CountryInfo>? Countries { get; set; }
        }

        public class CountryInfo
        {
            public string? Country { get; set; }
            public int Count { get; set; }
        }
    }
}