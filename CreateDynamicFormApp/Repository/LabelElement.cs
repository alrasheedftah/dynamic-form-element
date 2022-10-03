
namespace CreateDynamicFormApp.Repository;

public class LabelElement : IDomeElement
{
    private string _labelText ;
    public LabelElement(string LabelText){
        _labelText = LabelText;
    }

    public string getDomElemets()
    {
        return " <p> "+_labelText+" </p> ";
    }
}