using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace TestML
{
    public class Value
    {
        public List<string> ColumnNames { get; set; }
        public List<string> ColumnTypes { get; set; }
        public List<List<string>> Values { get; set; }
    }

    public class Output1
    {
        public string type { get; set; }
        public Value value { get; set; }
    }

    public class Results
    {
        public Output1 output1 { get; set; }
    }

    public class RootObject
    {
        public Results Results { get; set; }
    }

    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[,] Values { get; set; }
    }

    public class Result
    {
        public bool Success { get; set; }
        public double Lehtability { get; set; }
    }

  

    public class MLWrapper
    {
        public Result GetTelthy(string ls, string sex, int age, string embarkedFrom)
        {
            var myTask = Task.Run(() => this.GetLethability(ls, sex, age, embarkedFrom));
            // while the task is running you can do other things
            // after a while you need the result:
            var result = myTask.Result;
            return result;
        }

        private async Task<Result> GetLethability(string Class, string Sex, int Age, string EmbarkedFrom)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>()
                    {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"Survived", "Pclass", "Sex", "Age", "Embarked"},
                                Values = new string[,] {{"1", Class, Sex, Age.ToString(), EmbarkedFrom}},
                                //,  { "0", "0", "value", "0", "value" },  }
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey =
                    "gl3UXbfkxWwMfbe12RggeYO/lZeVaP11FZDlC75Ufmxvzj+vXxnvFOizAympoWlEgRR9FQXQqT27ksE/WzpPNQ==";
                // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress =
                    new Uri(
                        "https://ussouthcentral.services.azureml.net/workspaces/abefe921187546dbbdc849a1b5a36f0a/services/1d9d84b04f2f4f5ea380e8ab8698fa5e/execute?api-version=2.0&details=true");


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    
                    string startMatch = "Values\":[[";

                    if (result.IndexOf(startMatch) > 0)
                    {
                        result = result.Substring(result.IndexOf(startMatch) + startMatch.Length).TrimEnd(new char[]{'}'}).TrimEnd(new char[]{']'});
                        var split = result.Split(new string[] { "," }, StringSplitOptions.None);
                        var tmp =  Double.Parse(split[6].Trim(new char[] {'\"'}));
                        return new Result() { Lehtability = tmp, Success = true };
                    }
                    
                    return new Result() { Lehtability = 12.2, Success = true };
                    
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    return new Result() { Lehtability = 12.2, Success = true };
                }

            }
        }
    }
}
