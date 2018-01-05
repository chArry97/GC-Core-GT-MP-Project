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
		if (API.getEntityData(player.handle, "Adminlevel") >= 5) {
			VehicleHash vehicleHash = API.vehicleNameToModel(vehicle);
			if (vehicleHash != ((VehicleHash)0))
			{
				Vector3 vehPos = player.position;
				Vector3 vehRot = player.rotation;
				if(primaryColor > 159) { primaryColor = 0; }
				if(secondaryColor > 159) { secondaryColor = 0; }
				NetHandle vehHandle = API.createVehicle(vehicleHash, vehPos, new Vector3(0,0,vehRot.Z), primaryColor, secondaryColor);
				
				API.sendChatMessageToPlayer(player, "Dein Auto wurde gespawnt.");
				
				API.setPlayerIntoVehicle(player, vehHandle, -1);
			}
			else
			{
				API.sendChatMessageToPlayer(player, "Dein Auto wurde nicht gefunden.");
			}
		} else {
			API.sendNotificationToPlayer(player, "~w~Du hast nicht genügend Rechte, um diesen Befehl zu benutzen");
		}
    }
	
    [Command("vehh", Alias = "vehicleHash")]
    public void cmd_vehicleSpawnHash(Client player, int vehicle, int primaryColor = 0, int secondaryColor = 0)
    {
		if (API.getEntityData(player.handle, "Adminlevel") >= 5) {
            Vector3 vehPos = player.position;
            Vector3 vehRot = player.rotation;
            if (primaryColor > 159) { primaryColor = 0; }
            if (secondaryColor > 159) { secondaryColor = 0; }
			NetHandle vehHandle = API.createVehicle((VehicleHash) vehicle, vehPos, new Vector3(0, 0, vehRot.Z), primaryColor, secondaryColor);

            API.sendChatMessageToPlayer(player, "Dein Auto wurde gespawnt.");
			
			API.setPlayerIntoVehicle(player, vehHandle, -1);
		} else {
			API.sendNotificationToPlayer(player, "~w~Du hast nicht genügend Rechte, um diesen Befehl zu benutzen");
		}
    }
	
	[Command("repair")]
    public void cmd_repair(Client player)
    {
		if (API.getEntityData(player.handle, "Adminlevel") >= 5) {
			if (!API.isPlayerInAnyVehicle(player))
			{
				API.sendChatMessageToPlayer(player, "~r~System:~w~ Du sitzt nicht in einem Auto");
				return;
			}

			NetHandle vehicle = API.getPlayerVehicle(player);
			API.repairVehicle(vehicle);
		} else {
			API.sendNotificationToPlayer(player, "~w~Du hast nicht genügend Rechte, um diesen Befehl zu benutzen");
		}
    }
}
