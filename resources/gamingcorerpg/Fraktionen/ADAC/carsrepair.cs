using System;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;

//Das Anfangscript des Car Respair System.
public class Carsrepair : Script
{
    [Command("repair")]
    public void CMD_Repair(Client player)
    {
        if (!API.isPlayerInAnyVehicle(player))
        {
            API.sendChatMessageToPlayer(player, "~r~System:~w~ Du sitzt nicht in einem Auto");
            return;
        }

        NetHandle vehicle = API.getPlayerVehicle(player);
        API.repairVehicle(vehicle);
    }
}
