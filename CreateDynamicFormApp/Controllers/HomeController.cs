using System.Net.Http.Headers;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CreateDynamicFormApp.Models;
using Newtonsoft.Json;
using CreateDynamicFormApp.Repository.FormElement;
using CreateDynamicFormApp.Repository;
using CreateDynamicFormApp.Repository.DbAsJson;

namespace CreateDynamicFormApp.Controllers;

public class HomeController : Controller
{
    private readonly IFormElementRepository _formElement;
    private readonly IDbJsonRepository _dbJsonRepository;

    public HomeController(IFormElementRepository formElementRepository,IDbJsonRepository dbjsonRepository)
    {
        _formElement = formElementRepository;
        _dbJsonRepository = dbjsonRepository;
    }

    public IActionResult Index()
    {
        // var obj=JsonConvert.DeserializeObject<FormModel>(JsonFormatInput);
        // ViewBag.Name = string.Format("Name: {0} {1}", obj);
        return View();
    }

    [HttpPost]
    // [Produces("text/html")]
    public ContentResult CreateForm([FromBody]Testss JsonFormatInput)
    {
        var obj=JsonConvert.DeserializeObject<FormModelObject>(JsonFormatInput.JsonFormatInput);
        // var obj=JsonFormatInput.form;
        // return Ok(obj.form);
        var Form = obj.form;
        var StringHtml = "";
        // Console.WriteLine(obj);
        for (var i = 0; i < Form.Count; i++)
        {   
            // TODO To ADd Form Start Tag Elemnt With Style Class 
            StringHtml += _formElement.GeneratFormElement(new LabelElement(Form[i].Label));
            if(Form[i].Type == "text")
            StringHtml += _formElement.GeneratFormElement(new TextInputElement());
            else if(Form[i].Type == "checkbox" || Form[i].Type == "radio" )
                StringHtml += _formElement.GeneratFormElement(new OptionsElement(Form[i].Type,Form[i].options));
            // TODO To Set Speredted Row After Add Any Element In Object
        }

            // TODO To Return Json 
           var response = new ContentResult();
           response.Content = StringHtml;
           response.ContentType = "text/html";
           
        Int32 unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        var fileName = _dbJsonRepository.saveDataToJson(response.Content,unixTimestamp+"");
           response.Content = fileName;
        return response;
    }

    public IActionResult Privacy([FromQuery]string fileName)
    {
        // return Ok(fileName);
            try{

            var dataHtml= _dbJsonRepository.getData(fileName);
            ViewBag.Name = dataHtml; 
            }catch{
            ViewBag.Name = "<h1>  Please Be Sure The File Name Is True <h1>"; 

                // TOD To Redirect 500 PAge Here
            }

            return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
