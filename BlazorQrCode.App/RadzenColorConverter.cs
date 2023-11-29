using System.Drawing;
using System.Text.RegularExpressions;

namespace BlazorQrCode.AppWasm
{
    public static class RadzenColorConverter
    {
        public static Color Parse(string input)
        {
            if (input == null)
            {
                throw new ArgumentException("Parameter must not be null", nameof(input));
            }
            //rgb(68, 58, 110)

            // Step 1: Extract RGB values using regex
            Match match = Regex.Match(input, @"rgb\((\d+),\s*(\d+),\s*(\d+)\)");

            if (match.Success)
            {
                // Step 2: Convert extracted RGB values to integers
                int red = int.Parse(match.Groups[1].Value);
                int green = int.Parse(match.Groups[2].Value);
                int blue = int.Parse(match.Groups[3].Value);

                // Step 3: Create a new Color object
                Color color = Color.FromArgb(red, green, blue);

                //Console.WriteLine(color); // Output: Color [A=255, R=68, G=58, B=110]
                return color;
            }

            throw new ArgumentException("Wrong color format", nameof(input));

        }

        public static string ToString(Color color)
        {
            string rgbFormat = $"rgb({color.R}, {color.G}, {color.B})";
            return rgbFormat;
        }
    }
}
