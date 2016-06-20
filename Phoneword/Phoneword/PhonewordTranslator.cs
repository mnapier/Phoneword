using System.Text;

namespace Core
{
    public static class PhonewordTranslator
    {
        public static string ToNumber(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
            {
                return null;
            }

            raw = raw.ToUpperInvariant();

            var newNumber = new StringBuilder();
            foreach (var c in raw)
            {
                if (" -0123456789".Contains(c))
                {
                    newNumber.Append(c);
                }
                else
                {
                    var result = TranslateToNumber(c);
                    if (result != null)
                    {
                        newNumber.Append(result);
                    }
                    else
                    {
                        return null;
                    }
                }
            }

            return newNumber.ToString();
        }

        private static bool Contains(this string keyString, char c)
        {
            return keyString.IndexOf(c) >= 0;
        }

        private static readonly string[] Digits =
        {
            "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
        };

        private static int? TranslateToNumber(char c)
        {
            for (int i = 0; i < Digits.Length; i++)
            {
                if (Digits[i].Contains(c))
                {
                    return 2 + i;
                }
            }

            return null;
        }
    }
}
