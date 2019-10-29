using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using ConsoleApiCall.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ConsoleApiCall
{
  class Program
  {
    static void Main(string[] args)
    {
        DotNetEnv.Env.Load();
        string key = System.Environment.GetEnvironmentVariable("API_KEY");
        var apiCallTask = ApiHelper.ApiCall(key);
        var result = apiCallTask.Result;
        JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);
        List<Article> articleList = JsonConvert.DeserializeObject<List<Article>>(jsonResponse["results"].ToString());
        foreach (Article article in articleList)
                {
                    Console.WriteLine($"Section: {article.Section}");
                    Console.WriteLine($"Title: {article.Title}");
                    Console.WriteLine($"Abstract: {article.Abstract}");
                    Console.WriteLine($"Url: {article.Url}");
                    Console.WriteLine($"Byline: {article.Byline}");
                }

        var host = new WebHostBuilder()
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .Build();

        host.Run();
    }
  }

  class ApiHelper
  {
    public static async Task<string> ApiCall(string apiKey)
    {
      RestClient client = new RestClient("https://api.nytimes.com/svc/topstories/v2");
      RestRequest request = new RestRequest($"home.json?api-key={apiKey}", Method.GET);
      var response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }
  }
}
