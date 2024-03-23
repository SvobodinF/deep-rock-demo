public struct ViewportConfiguration : IConfiguration
{
    public readonly IAliveble Player;

    public ViewportConfiguration(IAliveble player)
    {
        Player = player;
    }
}
