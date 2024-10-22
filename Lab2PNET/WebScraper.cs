using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class WebScraper
{
    private static readonly HttpClient httpClient = new HttpClient();

    public async Task<string> PobierzZawartoscStronyAsync(string url)
    {
        try
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return content;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas pobierania zawartości strony: {ex.Message}");
            return string.Empty;
        }
    }

    public List<string> ZnajdzLinki(string content)
    {
        var links = new List<string>();
        var regex = new Regex(@"<a\s+(?:[^>]*?\s+)?href=([""'])(.*?)\1", RegexOptions.IgnoreCase);
        var matches = regex.Matches(content);

        foreach (Match match in matches)
        {
            string link = match.Groups[2].Value;
            if (Uri.IsWellFormedUriString(link, UriKind.RelativeOrAbsolute))
            {
                links.Add(link);
            }
        }

        return links;
    }

    public async Task PobierzZawartoscStronyIZalinkowanychPodstronAsync(string url)
    {
        string mainContent = await PobierzZawartoscStronyAsync(url);
        Console.WriteLine($"Zawartość strony głównej ({url}):\n{mainContent}\n");

        List<string> links = ZnajdzLinki(mainContent);
        foreach (string link in links)
        {
            string absoluteLink = link;
            if (!Uri.IsWellFormedUriString(link, UriKind.Absolute))
            {
                Uri baseUri = new Uri(url);
                Uri absoluteUri = new Uri(baseUri, link);
                absoluteLink = absoluteUri.ToString();
            }

            string subPageContent = await PobierzZawartoscStronyAsync(absoluteLink);
            Console.WriteLine($"Zawartość podstrony ({absoluteLink}):\n{subPageContent}\n");
        }
    }

    /*public static async Task Main(string[] args)
    {
        WebScraper scraper = new WebScraper();
        string url = "https://example.com"; // Zastąp przykładowym URL
        await scraper.PobierzZawartoscStronyIZalinkowanychPodstronAsync(url);
    }
    */
}
