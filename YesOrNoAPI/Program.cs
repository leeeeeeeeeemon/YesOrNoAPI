using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Threading;

namespace YesOrNoAPI
{
	public class AnswerApi

	{
        
        static public string Answer()
        {
            var url = $"https://yesno.wtf/api";
            var request = WebRequest.Create(url);

            var response = request.GetResponse();
            var httpStatusCode = (response as HttpWebResponse).StatusCode;

            if (httpStatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(httpStatusCode);
            }
            
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                var answer = JsonConvert.DeserializeObject<Root>(result);
                return (answer.answer + " " + answer.image);
            }
        }
    }
}
