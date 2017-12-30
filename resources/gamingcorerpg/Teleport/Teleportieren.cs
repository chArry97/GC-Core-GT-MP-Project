using System;
using System;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;

public class Teleportieren : Script
{
    [Command("tp", "Usage: /tp *Player*", GreedyArg = true)] // Admin teleportiert sich zum Spieler.
    public void teleportToPlayer(Client sender, Client target)
    {
        API.sendNotificationToPlayer(sender, "~w~Du wurdest zum Spieler Teleportiert.:~r~" + target.name);
        API.sendNotificationToPlayer(target, "~r~" + sender.name + " ~w~hat sich zu dir Teleportiert!");
        sender.position = target.position;
    }
    [Command("tptome", "Usage: /tptome *Player*", GreedyArg = true)] // Spieler wird zum Admin teleportiert.
    public void teleportPlayerToMe(Client sender, Client target)
    {
        Player player = sender.getData("player");
        if (player.Account.AdminLvl < 10)
            return;
        API.sendNotificationToPlayer(target, "~w~Du Teleportierst den Spieler zu dir:~r~" + sender.name);
        API.sendNotificationToPlayer(sender, "~r~" + target.name + " ~w~Du wurdest Teleportiert!");
        target.position = sender.position;
    }
}
