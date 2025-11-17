using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class CryptoSquare
{
    public static string NormalizedPlaintext(string plaintext)
    {
        return new string(plaintext
            .ToLowerInvariant()
            .Where(char.IsLetterOrDigit)
            .ToArray());
    }

    public static IEnumerable<string> PlaintextSegments(string plaintext)
    {
        var normalized = NormalizedPlaintext(plaintext);
        if (normalized.Length == 0) yield break;

        int c = (int)Math.Ceiling(Math.Sqrt(normalized.Length));
        for (int i = 0; i < normalized.Length; i += c)
            yield return normalized.Substring(i, Math.Min(c, normalized.Length - i));
    }

    public static string Encoded(string plaintext)
    {
        var segments = PlaintextSegments(plaintext).ToArray();
        if (segments.Length == 0) return "";

        int c = segments[0].Length;
        int r = segments.Length;
        var sb = new StringBuilder();

        for (int i = 0; i < c; i++)
        {
            for (int j = 0; j < r; j++)
            {
                if (i < segments[j].Length)
                    sb.Append(segments[j][i]);
            }
        }
        return sb.ToString();
    }

    public static string Ciphertext(string plaintext)
    {
        var segments = PlaintextSegments(plaintext).ToArray();
        if (segments.Length == 0) return "";

        int c = segments[0].Length;
        int r = segments.Length;
        var result = new List<string>();

        for (int i = 0; i < c; i++)
        {
            var sb = new StringBuilder();
            for (int j = 0; j < r; j++)
                sb.Append(i < segments[j].Length ? segments[j][i] : ' ');
            result.Add(sb.ToString());
        }

        return string.Join(" ", result);
    }
}
