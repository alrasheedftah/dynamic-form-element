using System.Linq;
using Xunit;
using CreateDynamicFormApp.Repository.FormElement;
using CreateDynamicFormApp.Repository;
using CreateDynamicFormApp.Repository.DbAsJson;
using Xunit.Priority;
using CreateDynamicFormApp.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace AppTest;

[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
public class UnitTest1
{

    IFormElementRepository formElementRepository;
    HomeController homeController;
    public UnitTest1(){
        homeController = new HomeController(new FormElementRepository(),new DbJsonRepository());
        formElementRepository = new FormElementRepository();
    }
    [Fact, Priority(0)]
    public void Test1()
    {
         Assert.Equal(4, 2+2);
    }


        [Fact, Priority(1)]
        public  void Should_Be_return_Label_valid()
        {
            //create In Memory Database
                //Arrange
               var _labelText = "Hello Alrasheed";
                var lable = formElementRepository.GeneratFormElement(new LabelElement(_labelText));
                //Assert
                Assert.IsType<string>(_labelText);
                //TODO Check Is Contain HTML Element 
                Assert.Equal(" <p> "+_labelText+" </p> ", lable);
        }

        [Fact, Priority(2)]
        public  void Should_Be_return_InputTypeText_valid()
        {
            //create In Memory Database
                //Arrange
                var textInputElement = formElementRepository.GeneratFormElement(new TextInputElement());
                //Assert
                Assert.IsType<string>(textInputElement);
                Assert.Equal("<input type='text' name='name'>", textInputElement);
        }


        [Fact, Priority(2)]
        public  void Should_Be_return_InputRedioOPtion_valid()
        {
            //create In Memory Database
                //Arrange
                var textInputElement = formElementRepository.GeneratFormElement(new OptionsElement("radio",new string[] { "element1", "element2", "element3" }));
                //Assert
                Assert.IsType<string>(textInputElement);
                Assert.Contains("radio",textInputElement);
                Assert.EndsWith("element3",textInputElement);
                Assert.Equal(3,textInputElement.Split("radio").Length-1);
                // Assert.Equal("<input type='text' name='name'>", textInputElement);

        }

        [Fact, Priority(2)]
        public  void Should_Be_return_InputCheckOPtion_valid()
        {
            //create In Memory Database
                //Arrange
                var textInputElement = formElementRepository.GeneratFormElement(new OptionsElement("checkbox",new string[] { "element1", "element2", "element3" }));
                //Assert
                Assert.IsType<string>(textInputElement);
                Assert.Contains("checkbox",textInputElement);
                Assert.EndsWith("element3",textInputElement);
                Assert.Equal(3,textInputElement.Split("checkbox").Length-1);//Cuz By Example The Split 1,1 return size 2 but actioly i have 2
                Assert.Equal(0,textInputElement.Split("radio").Length - 1); 
                // Assert.Equal("<input type='text' name='name'>", textInputElement.Split("radio")[1]);
        }


        [Fact, Priority(2)]
        public  void Should_Be_return_FullForm_From_Dynamic_Object_Json_valid()
        {
            //create In Memory Database
                //Arrange
                //TODo try To Change String To Dynamic Object Then Convert To Json Is Better 
                var jsonString = "{\n\t\"form\": [{\n\t\t\"type\": \"text\",\n\t\t\"label\": \"What is your name?\"\n\t},\n\t{\n\t\t\"type\": \"radio\",\n\t\t\"label\": \"What is your gender?\",\n\t\t\"options\": [\"Male\",\"Female\"]\n\t},\n\t{\n\t\t\"type\": \"checkbox\",\n\t\t\"label\": \"Select all your hobbies\",\n\t\t\"options\": [\"Music\",\"Movies\",\"Sports\"]\n\t}]\n}";

                ContentResult htmlForm = homeController.CreateForm(new CreateDynamicFormApp.Models.Testss{ JsonFormatInput = jsonString });


                var htmlFormExpected = @" <p> What is your name? </p> <input type='text' name='name'> <p> What is your gender? </p> <input type='radio' name='Male' value='Male'>Male<input type='radio' name='Female' value='Female'>Female <p> Select all your hobbies </p> <input type='checkbox' name='Music' value='Music'>Music<input type='checkbox' name='Movies' value='Movies'>Movies<input type='checkbox' name='Sports' value='Sports'>Sports";
                
                // REmove The White Space 
                var htmlFormActual = htmlForm.Content.Trim();
                htmlFormExpected = htmlFormExpected.Trim();

                //Assert
                Assert.IsType<string>(htmlFormActual);
                Assert.Contains("text",htmlFormActual);
                Assert.Contains("radio",htmlFormActual);
                Assert.Contains("checkbox",htmlFormActual);
                Assert.Contains("<p>",htmlFormActual);
                Assert.Equal(htmlFormExpected,htmlFormActual);//Cuz By Example The Split 1,1 return size 2 but actioly i have 2
                Assert.Equal(htmlFormExpected,htmlFormActual); 
                // Assert.Equal("<input type='text' name='name'>", textInputElement.Split("radio")[1]);
        }

}