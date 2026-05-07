namespace GMGInventory.Helpers
{
    public class AvatarHelper()
    {
        public static string GetColorFromString(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "#F0CFCF";

            int hash = 0;
            foreach (char c in input)
            {
                hash = c + ((hash << 5) - hash);
            }

            int r = (hash >> 0) & 0xFF;
            int g = (hash >> 8) & 0xFF;
            int b = (hash >> 16) & 0xFF;

            r = (r + 256) % 256;
            g = (g + 256) % 256;
            b = (b + 256) % 256;

            r = (r % 100) + 155;
            g = (g % 80) + 80;
            b = (b % 60) + 60;

            double brightness = (0.299 * r + 0.587 * g + 0.114 * b);
            if (brightness < 186)
            {
                r = (r + 255) / 2;
                g = (g + 255) / 2;
                b = (b + 255) / 2;
            }

            return $"#{r:X2}{g:X2}{b:X2}";
        }

        public static string InvertHex(string hex)
        {
            if (string.IsNullOrWhiteSpace(hex))
                return "#000000";

            hex = hex.Replace("#", "");

            if (hex.Length == 3)
            {
                hex = string.Concat(hex.Select(c => $"{c}{c}"));
            }

            if (hex.Length != 6)
                throw new ArgumentException("Invalid hex color");

            int r = Convert.ToInt32(hex.Substring(0, 2), 16);
            int g = Convert.ToInt32(hex.Substring(2, 2), 16);
            int b = Convert.ToInt32(hex.Substring(4, 2), 16);

            r = 255 - r;
            g = 255 - g;
            b = 255 - b;

            return $"#{r:X2}{g:X2}{b:X2}";
        }

        public static string GetInitials(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return "?";

            var parts = name.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);

            return string.Concat(parts.Take(3).Select(p => char.ToUpper(p[0])));
        }
    }
}
