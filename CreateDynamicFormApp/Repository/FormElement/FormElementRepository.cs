
namespace CreateDynamicFormApp.Repository.FormElement;

public class FormElementRepository : IFormElementRepository
{
    public string GeneratFormElement(IDomeElement domeElement)
    {
        return domeElement.getDomElemets();
    }
}