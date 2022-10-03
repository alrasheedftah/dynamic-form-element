using CreateDynamicFormApp.Models;


namespace CreateDynamicFormApp.Repository.DbAsJson;

public class DbJsonRepository : IDbJsonRepository
{
    public string getData(string fileName)
    {
        var data=JsonFileUtilsContext.getDataFromFile(fileName);  
        return data;      
    }

    public string saveDataToJson(string form, string fileName)
    {
        JsonFileUtilsContext.CreateFile(form,fileName);  
        return fileName;      
    }
}