using System;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;

public class CarCommands : Script 
{
    [Command("veh", Alias = "vehicle")]
    public void cmd_vehicleSpawn(Client player, string vehicle, int primaryColor = 0, int secondaryColor = 0)
    {
        VehicleHash vehicleHash = API.vehicleNameToModel(vehicle);
        if (vehicleHash != ((VehicleHash)0))
        {
            Vector3 vehPos = player.position;
            Vector3 vehRot = player.rotation;
            if(primaryColor > 159) { primaryColor = 0; }
            if(secondaryColor > 159) { secondaryColor = 0; }
            API.createVehicle(vehicleHash, vehPos, new Vector3(0,0,vehRot.Z), primaryColor, secondaryColor);
            
            API.sendChatMessageToPlayer(player, "Dein Auto wurde gespawnt.");
        }
        else
        {
            API.sendChatMessageToPlayer(player, "Dein Auto wurde nicht gefunden.");
        }
    }
	
    [Command("vehh", Alias = "vehicleHash")]
    public void cmd_vehicleSpawnHash(Client player, int vehicle, int primaryColor = 0, int secondaryColor = 0)
    {
            Vector3 vehPos = player.position;
            Vector3 vehRot = player.rotation;
            if (primaryColor > 159) { primaryColor = 0; }
            if (secondaryColor > 159) { secondaryColor = 0; }
            API.createVehicle((VehicleHash) vehicle, vehPos, new Vector3(0, 0, vehRot.Z), primaryColor, secondaryColor);

            API.sendChatMessageToPlayer(player, "Dein Auto wurde gespawnt.");
    }
	
	[Command("repair")]
    public void cmd_repair(Client player)
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
