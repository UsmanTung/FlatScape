using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

class Utils {
public static string PrettyPrint(Object o)
  {
    var jsonString = JsonConvert.SerializeObject(
         o, Formatting.Indented,
         new JsonConverter[] { new StringEnumConverter() });
    return jsonString;
  }
}