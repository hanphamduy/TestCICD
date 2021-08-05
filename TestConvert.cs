using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TestConverter
{
    private JToken ConvertHisObjectByJsonObject(JObject jObject, JToken hisObject)
    {
        //Nếu type là array
        if (hisObject.Type == JTokenType.Array)
        {
            var result = new JArray();
            foreach (var itemArr in hisObject)
            {
                var newItem = ConvertHisObjectByJsonObject(jObject, itemArr);
                result.Add(newItem);
            }

            return result;
        }
        //Nếu type là object
        else if (hisObject.Type == JTokenType.Object)
        {
            var hisObjectConverted = (JObject)hisObject;
            var result = new JObject(hisObjectConverted);
            foreach (var item in hisObjectConverted.Properties())
            {
                var newProperty = jObject.Properties().FirstOrDefault(i => i.Value.ToString() == item.Name);
                if (newProperty != null)
                {
                    result.Remove(item.Name.ToString());
                    result.Add(newProperty.Name, ConvertHisObjectByJsonObject(jObject, item.Value));
                }
            }

            return result;
        }
        else
        {
            return hisObject;
        }
    }

    /// <summary>
    /// Hàm convert từ json Object của His sang json Object của emr
    /// </summary>
    /// <param name="hisObject"></param>
    /// <param name="pluginName"></param>
    /// <returns></returns>
    public JToken ConvertHisObjectToEmrObject(JToken hisObject)
    {
        try
        {
            var jsonObject = new JObject();

            using (StreamReader file = File.OpenText(@"./plugins/thongtingiaychuyenvien/Converter.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                jsonObject = (JObject)serializer.Deserialize(file, typeof(JObject));
            }

            var result = ConvertHisObjectByJsonObject(jsonObject, hisObject);

            return result;
        }
        catch(Exception ex)
        {
            throw new Exception("Lỗi ", ex);
        }
    }

}
