using System;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;

public class Cars : Script
{
    [Command("veh", Alias = "Vehicle")]//Command führt aus das /veh oder /vehicle das Auto Gespawnt wird.
    public void veh(Client player, string vehicle, int color1 = 0, int color2 = 0) //int Hash = Deklaration des namens
    {
        VehicleHash myVehicle = API.vehicleNameToModel(vehicle); //Spawnt durch string vehicle jegliches model was ich will.
        if (myVehicle != ((VehicleHash)0))
        {
            Vector3 vehPos = player.position; //die Definition 
            Vector3 vehRot = player.rotation;
            if(color1 > 159) { color1 = 0; } //Größer als 159 erstellt 0 = Schwarz
            if(color2 > 159) { color2 = 0; }
            API.createVehicle(myVehicle, vehPos, new Vector3(0,0,vehRot.Z), color1, color2);
            
            API.sendChatMessageToPlayer(player, "Dein Auto wurde gespawnt.");
        }
        else
        {
            API.sendChatMessageToPlayer(player, "Dein Auto wurde nicht gefunden.");
        }
        
    }
    [Command("vehh", Alias = "HashVehicle")]//Command führt aus das /veh oder /vehicle das Auto Gespawnt wird.
    public void vehh(Client player, int vehicle, int color1 = 0, int color2 = 0) //int Hash = Deklaration des namens
    {

            Vector3 vehPos = player.position; //die Definition 
            Vector3 vehRot = player.rotation;
            if (color1 > 159) { color1 = 0; } //Größer als 159 erstellt 0 = Schwarz
            if (color2 > 159) { color2 = 0; }
            API.createVehicle((VehicleHash) vehicle, vehPos, new Vector3(0, 0, vehRot.Z), color1, color2);

            API.sendChatMessageToPlayer(player, "Dein Auto wurde gespawnt.");
        
    }
}
