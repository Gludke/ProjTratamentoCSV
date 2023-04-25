using System.Text.RegularExpressions;

namespace Proj.Shared
{
    public static class Extensions
    {
        public static string RemoveAccents(this string txt)
        {
            return Regex.Replace(txt, "[áàâä]", "a")
               .Replace("é", "e")
               .Replace("è", "e")
               .Replace("ê", "e")
               .Replace("ë", "e")
               .Replace("í", "i")
               .Replace("ì", "i")
               .Replace("î", "i")
               .Replace("ï", "i")
               .Replace("ó", "o")
               .Replace("ò", "o")
               .Replace("ô", "o")
               .Replace("ö", "o")
               .Replace("ú", "u")
               .Replace("ù", "u")
               .Replace("û", "u")
               .Replace("ü", "u")
               .Replace("ç", "c")
               .Replace("ã", "a")
               .Replace("õ", "o");
        } 
    }
}
