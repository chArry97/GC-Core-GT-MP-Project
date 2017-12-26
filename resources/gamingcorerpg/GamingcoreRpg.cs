using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;

public class GamingcoreRpg : Script {
    private static API _api;

    public static API api => _api;

    public GamingcoreRpg()
    {
        _api = API;
    }
}