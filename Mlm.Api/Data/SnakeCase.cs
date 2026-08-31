using System.Text;

namespace Mlm.Api.Data;

internal static class SnakeCase
{
    public static string FromPascal(string value)
    {
        if (value.Length == 0)
        {
            return value;
        }

        var builder = new StringBuilder(value.Length + 4);
        builder.Append(char.ToLowerInvariant(value[0]));
        for (var i = 1; i < value.Length; i++)
        {
            var c = value[i];
            if (char.IsUpper(c))
            {
                builder.Append('_');
                builder.Append(char.ToLowerInvariant(c));
            }
            else
            {
                builder.Append(c);
            }
        }

        return builder.ToString();
    }
}
