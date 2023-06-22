namespace GamingApi.Games;
public static class Connections
{
    public static Connection SteamGamesFeed = new(
        "yld.gamesfeed",
        @"https://yld-recruitment-resources.s3.eu-west-2.amazonaws.com",
        @"steam_games_feed.json"
    );
}
