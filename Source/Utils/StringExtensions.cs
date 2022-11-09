
namespace ScenarioDB;

public static class StringExtensions
{
    public static string ToPascalCase(this string word)
    {
        return string.Join("" , word.Split('_')
                 .Select(w => w.Trim())
                 .Where(w => w.Length > 0)
                 .Select(w => w.Substring(0,1).ToUpper() + w.Substring(1).ToLower()));
    }
}