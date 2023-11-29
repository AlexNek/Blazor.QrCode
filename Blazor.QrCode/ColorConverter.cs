using System.Drawing;
using System.Text;

namespace Blazor.QrCode
{
    public static class ColorConverter
    {
        public static string Convert(Color color)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("#");
            sb.Append(color.R.ToString("x2"));
            sb.Append(color.G.ToString("x2"));
            sb.Append(color.B.ToString("x2"));
            return sb.ToString();
        }
    }
}
