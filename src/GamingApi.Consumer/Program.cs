

using GamingApi.Consumer.Contracts;
using GamingApi.Consumer.Response;

var groups = await GetGamesByPublisherAsync();
Console.WriteLine($"{string.Join("| ", groups.Where(p => p.Value.Length > 1).Select(p => $"{p.Key}-#-{p.Value.Length}"))}");

//OptionPattern
//Option<T>
//Option<IEnumerable<GameDto>>

//var option = GetGames()
//option switch{
//IEnumerable<GameDto> => processSuccess,
//_=> processError
//}
async IAsyncEnumerable<GameDto> GetGamesAsync()
{

    var http = new HttpClient()
    {
        DefaultRequestHeaders = {
        {"User-Agent", "console.app" }
        }
    };

    const int ItemsPerPage = 10;
    async Task<GamesContract> get_pageAsync(int page)
    {

        var response = await http.GetAsync($"https://laamx3l4hq65zwwdziyid2q3tu0rplrh.lambda-url.eu-west-2.on.aws/api/games?offset={page * ItemsPerPage}&limit={ItemsPerPage}");

        var content = await response.Content.ReadAsStringAsync();

        var contract = System.Text.Json.JsonSerializer.Deserialize<GamesContract>(content)!;

        return contract;
    }

    var page = 0;

    var contract = await get_pageAsync(page);

    var remainingItemsToQuery = contract.TotalItems - contract.Items.Length;

    foreach (var item in contract.Items)
        yield return item;

    while (remainingItemsToQuery > 0)
    {
        contract = await get_pageAsync(++page);

        remainingItemsToQuery -= contract.Items.Length;

        foreach (var item in contract.Items)
            yield return item;

    }

}

async Task<Dictionary<string, PublisherGameResponse[]>> GetGamesByPublisherAsync()
{
    var allGames = GetGamesAsync().GetAsyncEnumerator();

    var dic = new Dictionary<string, PublisherGameResponse[]>();
    while (await allGames.MoveNextAsync())
    {
        var newElem = new[] {
                new PublisherGameResponse(allGames.Current)
        }!;

        if (dic.TryGetValue(allGames.Current.Publisher, out var content))
            dic[allGames.Current.Publisher] = content.Concat(newElem).ToArray();
        else
            dic.Add(allGames.Current.Publisher, newElem);
    }

    return dic;
}

