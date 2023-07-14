using GamingApi.Consumer.Contracts;

namespace GamingApi.Consumer.Response;

//"Choice of Games":[
//      {
//         "Name": "Chronicon Apocalyptica",
//         "ShortDescription": "Battle Norse raiders, ghosts, and changelings to save medieval England! But beware, if the elves can capture the Book you hold, the world will end.",
//         "Genre": [
//            "Adventure",
//            "Indie",
//            "RPG"
//         ],
//         "Platforms": [
//            "Windows",
//            "Mac",
//            "Linux"
//         ],
//         "ReleaseDate": "2019-01-11T03:48:00"
//      },
internal sealed class PublisherGameResponse
{
    private GameDto _current;

    public PublisherGameResponse(GameDto current)
    {
        Name = current.Name;
        ShortDescription = current.ShortDescription;
        Genre = current.Genre.Split(", ");
        Platforms = current.Platforms.Where(p => p.Value).Select(p => p.Key).ToArray();
        ReleaseDate = current.ReleaseDate.ToString();
    }

    public string Name { get; init; } = string.Empty;

    public string ShortDescription { get; init; } = string.Empty;


    public string[] Genre { get; init; } = Array.Empty<string>();

    public string[] Platforms { get; init; } = Array.Empty<string>();

    public string ReleaseDate { get; init; } = string.Empty;

}
