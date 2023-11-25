using System.Drawing;
using Console = Colorful.Console;

namespace OpenCCTV.Interface
{
    /// <summary>
    /// Provides methods for the interface.
    /// </summary>
    public class InterfaceMaker
    {
        /// <summary>
        /// Represents the ASCII 'header' for the application.
        /// </summary>
        private static readonly string[] _asciiLogo =
        {
            "                                                                     ",
            " ██████╗ ██████╗ ███████╗███╗   ██╗ ██████╗ ██████╗████████╗██╗   ██╗",
            "██╔═══██╗██╔══██╗██╔════╝████╗  ██║██╔════╝██╔════╝╚══██╔══╝██║   ██║",
            "██║   ██║██████╔╝█████╗  ██╔██╗ ██║██║     ██║        ██║   ██║   ██║",
            "██║   ██║██╔═══╝ ██╔══╝  ██║╚██╗██║██║     ██║        ██║   ╚██╗ ██╔╝",
            "╚██████╔╝██║     ███████╗██║ ╚████║╚██████╗╚██████╗   ██║    ╚████╔╝ ",
            " ╚═════╝ ╚═╝     ╚══════╝╚═╝  ╚═══╝ ╚═════╝ ╚═════╝   ╚═╝     ╚═══╝  ",
            "                                                                     ",
        };

        /// <summary>
        /// Represents the colors for the ASCII logo.
        /// </summary>
        private static readonly List<Color> _colorList = new List<Color>()
        {
            Color.White,
            ColorTranslator.FromHtml("#FCEE2B"),
            ColorTranslator.FromHtml("#E2D626"),
            ColorTranslator.FromHtml("#C8BD21"),
            ColorTranslator.FromHtml("#AFA51D"),
            ColorTranslator.FromHtml("#958C18"),
            ColorTranslator.FromHtml("#7B7413"),
        };

        /// <summary>
        /// Prints the ASCII logo to the console with customizable colors (scheme set to yellow -> black).
        /// </summary>
        public static void PrintLogo()
        {
            for (int i = 0; i < _asciiLogo.Length - 1; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - _asciiLogo[0].Length / 2, i);
                Console.WriteLine(_asciiLogo[i], _colorList[i]);

                switch (i)
                {
                    case 1:
                        Colors.Primary = _colorList[i];
                        break;

                    case 2:
                        Colors.Secondary = _colorList[i];
                        break;
                }
            }

            // Bottom padding.
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        /// <summary>
        /// Override method to display formatted and colorized WriteLine.
        /// </summary>
        /// <param name="color">The color of the message.</param>
        /// <param name="prefix">The prefix of the message.</param>
        /// <param name="msg">The message to be displayed.</param>
        public static void WriteLine(Color color, string prefix, string msg)
        {
            Console.Write(" [", Colors.Secondary);
            Console.Write(prefix, Color.White);
            Console.Write("]", Colors.Secondary);
            Console.Write(" → ", Color.White);
            Console.Write(msg, color);
        }

        /// <summary>
        /// Method to print country information formatted and colorized.
        /// </summary>
        /// <param name="countryCode">The two characters to identify a country.</param>
        /// <param name="countryName">The name of the country.</param>
        /// <param name="count">The count of cameras.</param>
        public static void PrintCountryInfo(string countryCode, string countryName, int count)
        {
            Console.Write(" [", Colors.Secondary);
            Console.Write($"{countryCode}", Color.White);
            Console.Write("]", Colors.Secondary);
            Console.Write(" → ", Color.White);
            Console.Write($"{countryName}", Colors.Primary);
            Console.Write(" → ", Color.White);

            switch (count == 1)
            {
                case true:
                    Console.WriteLine(count + " Camera", Color.White);
                    break;

                case false:
                    Console.WriteLine(count + " Cameras", Color.White);
                    break;
            }
        }
    }

    /// <summary>
    /// Provides Color variables for the interface.
    /// </summary>
    public class Colors
    {
        public static Color Primary { get; set; }
        public static Color Secondary { get; set; }
    }
}