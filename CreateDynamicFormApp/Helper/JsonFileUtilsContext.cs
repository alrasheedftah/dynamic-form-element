public static class JsonFileUtilsContext
{
    // TODO To Be Json Format Neater HTML To Allowed The Answer For Users
    public static void CreateFile(string form, string fileName)
    {
         // TODO To Save In WWWroot Is Best And Secure
         File.WriteAllText(fileName, form);
    }

    public static string getDataFromFile(string fileName)
    {
         var htmlString = File.ReadAllText(fileName);
         return htmlString;
    }    
}