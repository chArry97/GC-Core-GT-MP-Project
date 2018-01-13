using System;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;

public class MapCommands : Script
{
    [Command("adminmap", "Usage: /adminmap ", Alias = "am", GreedyArg = true)]
    public void cmd_teleport(Client sender, Client target) {
        if (API.getEntityData(sender.handle, "Adminlevel") >= 5)
        {
            API.sendNotificationToPlayer(sender, "~w~Du wurdest zum Spieler teleportiert: ~r~" + target.name);
            API.sendNotificationToPlayer(target, "~r~" + sender.name + " ~w~hat sich zu dir teleportiert!");
            sender.position = target.position;
        }
        else
        {
            API.sendNotificationToPlayer(sender, "~w~Du hast nicht genügend Rechte, um diesen Befehl zu benutzen");
        }
    }
}