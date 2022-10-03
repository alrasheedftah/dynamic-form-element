
namespace CreateDynamicFormApp.Repository;

public class TextInputElement : IDomeElement
{
    public string getDomElemets()
    {
        return "<input type='text' name='name'>";
    }
}