using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

public class Program
{
    public static void Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = getTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static int getTotalScoredGoals(string team, int year)
    {
        var uri = "https://jsonmock.hackerrank.com/";
        var url = "api/football_matches";
        var time = 1;
        var gols = 0;

        do
        {
            var listaRetorno = new List<Data>();
            var page = 1;
            do
            {
                var urlBusca = url;
                if (!string.IsNullOrEmpty(team))
                    urlBusca = validationURL(urlBusca, $"team{time}={team}");
                if (year > 0)
                    urlBusca = validationURL(urlBusca, $"year={year}");

                urlBusca = validationURL(urlBusca, $"page={page}");

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(uri);
                var resultado = client.GetAsync(urlBusca).Result;
                if (resultado.IsSuccessStatusCode)
                {
                    var stringResultado = resultado.Content.ReadAsStringAsync().Result;

                    Retorno retorno = JsonConvert.DeserializeObject<Retorno>(stringResultado);

                    listaRetorno.AddRange(retorno.Data);

                    if (retorno.total_pages > retorno.page)
                        page++;
                    else
                        break;
                }
                else
                {
                    throw new HttpRequestException(resultado.ReasonPhrase);
                }
            } while (true);

            foreach (var item in listaRetorno)
                if (time == 1)
                    gols += item.team1goals;
                else if (time == 2)
                    gols += item.team2goals;

            if (time == 2)
                break;
            else
                time++;
        } while (true);

        return gols;
    }

    private static string validationURL(string url, string parametro)
    {
        if (url.IndexOf("?") < 0)
            url += "?";
        else
            url += "&";

        return url += parametro;
    }

    private class Retorno
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public List<Data> Data { get; set; }
    }

    private class Data
    {
        public string competition { get; set; }
        public int year { get; set; }
        public string round { get; set; }
        public string team1 { get; set; }
        public string team2 { get; set; }
        public int team1goals { get; set; }
        public int team2goals { get; set; }
    }
}