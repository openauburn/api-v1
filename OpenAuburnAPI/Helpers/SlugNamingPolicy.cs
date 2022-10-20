using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace open_auburn_api.Helpers
{
    public class SlugNamingPolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            return Regex.Replace(name,
                "([a-z])([A-Z])",
                "$1_$2",
                RegexOptions.CultureInvariant,
                TimeSpan.FromMilliseconds(100)).ToLowerInvariant(); ;
        }
    }
}
