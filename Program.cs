using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Numerics;

namespace WebAPIClient
{
    class TVShowOuter
    {
        [JsonProperty("score")]
        public float Score { get; set; }
        [JsonProperty("show")]
        public TVShowData Show { get; set; }
    }

    class TVShowData
    {
        [JsonProperty("name")]
        public string ShowName { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; }
        [JsonProperty("premiered")]
        public string PremiereDate { get; set; }
        [JsonProperty("ended")]
        public string EndDate { get; set; }
    }

    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            await ProcessRepositories();
        }
        private static async Task ProcessRepositories()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter a TV show name, press enter without writing a name to quit the program. ");
                    var TVShowName = Console.ReadLine();
                    if (string.IsNullOrEmpty(TVShowName))
                    {
                        break;
                    }

                    var result = await client.GetAsync(" https://api.tvmaze.com/search/shows?q=" + TVShowName);
                    var resultRead = await result.Content.ReadAsStringAsync();
                    var deserialized = JsonConvert.DeserializeObject<List<TVShowOuter>>(resultRead);
                    Console.WriteLine("--------");
                    Console.WriteLine("Name: " + deserialized[0].Show.ShowName);
                    Console.WriteLine("Language: " + deserialized[0].Show.Language);
                    Console.WriteLine("Date Premiered: " + deserialized[0].Show.PremiereDate);
                    Console.WriteLine("Date Ended: " + deserialized[0].Show.EndDate);
                    Console.WriteLine("\n--------");
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR. Please enter a valid TV show name");
                }
            }
        }
    }

}

