using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace blazorlocalizationnofityservicepoc.Services
{
    public class LocaleService
    {
        public static List<Locale> Locales { get; set; } = new();

        public static bool Loaded { get; set; } = false;

        public static string SelectedLocale { get; set; } = "en";


        private readonly HttpClient _httpClient;


        public LocaleService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task Load()
        {
            if (!Loaded)
            {
                string data = await _httpClient.GetStringAsync("https://localhost:7176/locale.json");
                Locales = JsonConvert.DeserializeObject<List<Locale>>(data);
                Loaded = true;
            }
        }

        public void UpdateLocale(string locale)
        {
            SelectedLocale = locale;
        }


        public string GetByKey(string key)
        {
            if (Loaded)
            {
                return Locales.Single(x => x.key == key).translations.Single(x => x.language == SelectedLocale).value;
            }

            return String.Empty;
        }
    }

    public class Locale
    {
        public string key { get; set; }
        public List<Translation> translations { get; set; }
    }

    public class Translation
    {
        public string language { get; set; }
        public string value { get; set; }
    }


}
