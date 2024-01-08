using System.Reflection;
using System.Text;

namespace Reflection
{
    public class CsvConvert
    {        
        public static string Serialize<T>(T obj)
        {
            var properties = typeof(T).GetProperties();
            var sb = new StringBuilder();

            foreach (var property in properties)
            {
                sb.Append($"{property.Name}:{property.GetValue(obj)},");
            }
            //Console.WriteLine( sb.ToString() );
            return sb.ToString().TrimEnd(' ', ',');
        }
        public static T Deserialize<T>(string data)
        {
            var instance = Activator.CreateInstance<T>();
            var propertyValues = data.Split(',');
            foreach (var propertyValue in propertyValues)
            {
                var keyValue = propertyValue.Split(':');
                var key = keyValue[0];
                var value = keyValue[1];
                PropertyInfo? piShared = typeof(T).GetProperty(key);
                piShared?.SetValue(instance, Convert.ChangeType(value, piShared.PropertyType));
            }
            return instance;
        }
    }
}
