using System.ComponentModel.DataAnnotations;
namespace CreateDynamicFormApp.Models;


public class Testss
{
    public string JsonFormatInput {get;set;}

}

public class FormModelObject
{
    public List<FormModel> form {get;set;}
}


public class FormModel
{

    [RequiredAttribute]
    public string Type { get; set; }

    public string? Label { get; set; }
    public IList<string>? options { get; set; }

}
