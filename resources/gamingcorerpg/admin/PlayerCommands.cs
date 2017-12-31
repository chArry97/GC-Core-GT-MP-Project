using System;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;

public class PlayerCommands : Script
{
    [Command("tp", "Usage: /tp *Player*", Alias = "teleport", GreedyArg = true)]
    public void cmd_teleport(Client sender, Client target)
    {
        API.sendNotificationToPlayer(sender, "~w~Du wurdest zum Spieler teleportiert.:~r~" + target.name);
        API.sendNotificationToPlayer(target, "~r~" + sender.name + " ~w~hat sich zu dir teleportiert!");
        sender.position = target.position;
    }
	
    [Command("tphere", "Usage: /tptome *Player*", Alias = "teleportHere", GreedyArg = true)]
    public void cmd_teleportHere(Client sender, Client target)
    {
        API.sendNotificationToPlayer(target, "~w~Du teleportierst den Spieler zu dir:~r~" + sender.name);
        API.sendNotificationToPlayer(sender, "~r~" + target.name + " ~w~Du wurdest teleportiert!");
        target.position = sender.position;
    }
	
    [Command("tppos", "Usage: /tppos *Player*", Alias = "teleportToPosition", GreedyArg = true)]
    public void cmd_teleportHere(Client sender, float x, float y, float z)
    {
        API.sendNotificationToPlayer(sender, "~w~Du wurdest teleportiert!");
        sender.position = new Vector3(x, y, z);
    }
}
