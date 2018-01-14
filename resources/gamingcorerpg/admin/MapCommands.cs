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
    public void cmd_mapMenu(Client sender) {
        if (API.getEntityData(sender.handle, "Mappinglevel") >= 3)
        {
            API.triggerClientEvent(sender, "createMapMenu");
        }
        else
        {
            API.sendNotificationToPlayer(sender, "~w~Du hast nicht genügend Rechte, um diesen Befehl zu benutzen");
        }
    }
}