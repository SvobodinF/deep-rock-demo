public struct ViewportConfiguration : IConfiguration
{
    public readonly IAliveble Player;
    public readonly OreHandler OreHandler;

    public ViewportConfiguration(IAliveble player, OreHandler oreHandler)
    {
        Player = player;
        OreHandler = oreHandler;
    }
}
