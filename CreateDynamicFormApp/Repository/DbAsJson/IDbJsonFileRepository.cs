using CreateDynamicFormApp.Models;


namespace CreateDynamicFormApp.Repository.DbAsJson;

public interface IDbJsonRepository
{
    public string saveDataToJson(string form,string fileName);
    public string getData(string fileName);
}