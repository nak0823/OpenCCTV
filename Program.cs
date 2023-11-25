using System.Diagnostics;
using System.Drawing;

namespace OpenCCTV
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Title = "CCTV Browser by Serialized";
            Console.CursorVisible = false;

            OpenMenu();
            Console.ReadKey(); /* Prevent from closing */
        }

        /// <summary>
        /// Displays the main menu for OpenCCTV.
        /// </summary>
        private static void OpenMenu()
        {
            Console.Clear();
            Interface.InterfaceMaker.PrintLogo();
            Interface.InterfaceMaker.WriteLine(Color.White, "~", "Welcome to OpenCCTV - Select An Option\n\n");
            Interface.InterfaceMaker.WriteLine(Color.White, "1", "View Countries\n");
            Interface.InterfaceMaker.WriteLine(Color.White, "2", "Goto Repository\n");

            ConsoleKey cki = getCki;

            switch (cki)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    CCTV.Utils.CountryMenu();
                    break;

                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    OpenWebpage("https://github.com/nak0823/OpenCCTV"); // Star the repo ;)
                    OpenMenu();
                    break;

                default:
                    OpenMenu();
                    break;
            }
        }

        /// <summary>
        /// Opens a webpage using cmd.
        /// </summary>
        /// <param name="url"></param>
        public static void OpenWebpage(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "cmd",
                    Arguments = $"/c start {url}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Returns the users selected ConsoleKey.
        /// </summary>
        private static ConsoleKey getCki => Console.ReadKey(true).Key;
    }
}