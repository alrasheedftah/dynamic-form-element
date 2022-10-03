
namespace CreateDynamicFormApp.Repository;

public class OptionsElement : IDomeElement
{
    private string _name;
    private IList<string> _options;

    public OptionsElement(string name,IList<string> options){
        _name = name;
        _options = options;
    }
    public string getDomElemets()
    {
        var HtmlStatementString = "";
        for (var i = 0; i < _options.Count; i++)
        {
            HtmlStatementString += "<input type='"+_name+"' name='"+_options[i]+"' value='"+_options[i]+"'>"+_options[i];
        }
        return HtmlStatementString;
    }
}