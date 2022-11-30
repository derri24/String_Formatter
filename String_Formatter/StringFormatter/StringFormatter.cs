
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

    private string Replace(string template, Dictionary<string, string> data)
    {
        int countOpenBrackets = 0;
        int i = 0;
        var tempParameter = "";

        while (i < template.Length)
        {
            if (template[i] == '{')
                countOpenBrackets++;
            else
            {
                if (countOpenBrackets == 1)
                    if (template[i] != '}')
                        tempParameter += template[i];
                if (template[i] == '}')
                {
                    if (data.ContainsKey(tempParameter))
                    {
                        var startIndex = i - tempParameter.Length;
                        var value = data[tempParameter];

                        template = template.Remove(startIndex - 1, tempParameter.Length + 2);
                        template = template.Insert(startIndex - 1, value);
                        i -= startIndex+Math.Abs(tempParameter.Length-value.Length)+1;
                    }
                    tempParameter = "";
                    countOpenBrackets = 0;
                }
            }
            i++;
        }
        template = template.Replace("{{", "{");
        template = template.Replace("}}", "}");
        return template;
    }
    private Dictionary<string, string> CreateDataDictionary(List<string> dataForReplace, object target)
    {
        Dictionary<string, string> data = new Dictionary<string, string>();
        foreach (var element in dataForReplace)
        {
            var expression = ExpressionCreator.Create(target.GetType(), element);
            var value = expression(target);
            data.Add(element, value.ToString());
        }
        return data;
    }
    private bool CheckBracketsBalance(string template)
    {
        var countOpenBrackets = template.Count(ch=>ch=='{');
        var countCloseBrackets = template.Count(ch=>ch=='}');
        return countOpenBrackets == countCloseBrackets;
    }
    public string Format(string template, object target)
    {
        if (!CheckBracketsBalance(template))
            throw new ArgumentException("The string contains unbalanced curly braces!");
        var dataForReplace = GetListOfPropertiesAndFields(target);
        var data = CreateDataDictionary(dataForReplace, target);

        return Replace(template, data);
    }

}