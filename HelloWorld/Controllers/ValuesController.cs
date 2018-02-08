using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpPost]
        public SkillResponse Post([FromBody]SkillRequest input)
        {
            var requestType = input.GetRequestType();
            SkillResponse response = null;
            if (requestType == typeof(LaunchRequest))
            {
                response = Speak("Hi, welcome to greeter! To get greeted say greet!");
            }
            else if (requestType == typeof(IntentRequest))
            {
                var intentRequest = input.Request as IntentRequest;

                // check the name to determine what you should do
                if (intentRequest.Intent.Name.Equals("GreeterIntent"))
                {
                    response = Speak("Greeter intent initiated");
                }
                else if(intentRequest.Intent.Name.Equals("AMAZON.HelpIntent")){
                    response = Speak("What would you like to know?");
                }
            }

            response.Response.ShouldEndSession = false;
            return response;
        }

        private SkillResponse Speak(string message)
        {
            // build the speech response 
            var speech = new SsmlOutputSpeech();
            speech.Ssml = "<speak>" + message + "</speak>";

            // create the response using the ResponseBuilder
            var welcomeResponse = ResponseBuilder.Tell(speech);
            return welcomeResponse;
        }

        [HttpGet]
        public string Get()
        {
            return "Alexa API";
        }
    }
}
