namespace String_Formatter;

public class StringFormatter : IStringFormatter
{
    public static readonly StringFormatter Shared = new StringFormatter();

    private List<string> GetListOfPropertiesAndFields(object target)
    {
        List<string> data = new List<string>();
        Type type = target.GetType();
        foreach (var property in type.GetProperties())
            data.Add(property.Name);
        foreach (var field in type.GetFields())
            data.Add(field.Name);
        return data;
    }

    public void Replace(string template, Dictionary<string, string> data)
    {
        int countOpen = 0;
        for (int i = 0; i < template.Length; i++)
        {
            for (int j = 0; j < template.Length; j++)
            {
                if (template[i] == template[j] && template[j] == '{')
                {
                
                } 
            }
        }
    }

    //экранирование
    private Dictionary<string, string> CreateDataDictionary(List<string> dataForReplace, object target)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        //string result = template;
        foreach (var element in dataForReplace)
        {
            var value = target.GetType().GetProperty(element)?.GetValue(target, null);
            data.Add(element, value.ToString());
            // result = result.Replace("{" + element + "}", value.ToString());
        }

        return data;
    }

    public string Format(string template, object target)
    {
        var dataForReplace = GetListOfPropertiesAndFields(target);
        var data = CreateDataDictionary(dataForReplace, target);
        Replace(template, data);
        return null;
    }
}