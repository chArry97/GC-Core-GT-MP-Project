using System;
using System.Collections.Generic;
using System.Text;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;

public class Atm
{
    private CylinderColShape col;
    private NetHandle ped;

    public Atm(Vector3 colPos, float heading)
    {
        InitiateAtm(colPos, heading);
    }

    private void InitiateAtm(Vector3 colPos, float heading)
    {
        col = API.shared.createCylinderColShape(colPos, 0.8f, 2.0f);
        col.dimension = 0;
        col.onEntityEnterColShape += OnAtmTriggerEnter;
        Blip blip = API.shared.createBlip(colPos);
        blip.color = 49;
        blip.name = "ATM";
        blip.scale = 0.6f;
        blip.sprite = 207;
        API.shared.createMarker(1, new Vector3(colPos.X, colPos.Y, colPos.Z - 1f), new Vector3(), new Vector3(), new Vector3(1, 1, 0.5f), 255, 225, 59, 59);
    }

    private void OnAtmTriggerEnter(ColShape shape, NetHandle entity)
    {
        if (shape == col)
        {
            Client player = API.shared.getPlayerFromHandle(entity);
            if (player == null)
            {
                return;
            }

            if (!player.isInVehicle)
                API.shared.triggerClientEvent(player, "atmOpen");
        }
    }
}
