using System;
using System.Data;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;

public class SummerBikeShop {
	
	private CylinderColShape col;
	private NetHandle ped;
	
	public SummerBikeShop(Vector3 colPos,  float heading) {
		initiateBikeRental(colPos, heading);
	}
	
	private void initiateBikeRental(Vector3 colPos,  float heading) {
		col = API.shared.createCylinderColShape(colPos, 1.0f, 2.0f);
		col.dimension = 0;
		col.onEntityEnterColShape += onBikeRentalTriggerEnter;
		Blip blip = API.shared.createBlip(colPos);
		blip.color = 3;
		blip.name = "Summer Bike Fahrradverleih";
		blip.scale = 0.8f;
		blip.sprite = 226;
		API.shared.createMarker(1, new Vector3(colPos.X, colPos.Y, colPos.Z-1f), new Vector3(), new Vector3(), new Vector3(2, 2, 0.5f), 255, 99, 198, 213);
	}
	
	private void onBikeRentalTriggerEnter(ColShape shape, NetHandle entity) {
		if (shape == col) {
			Client player = API.shared.getPlayerFromHandle(entity);
			if (player == null) {
				return;
			}

            if (!player.isInVehicle)
                API.shared.triggerClientEvent(player, "bikeShopOpen");
            else if (API.shared.hasEntityData(player.vehicle, "Loaned"))
                if (API.shared.getEntityData(player.vehicle, "Loaned"))
                {
                    API.shared.triggerClientEvent(player, "bikeReturnOpen");
                }
		}
	}
	
	private void stopBikeRental() {
		API.shared.deleteEntity(ped);
	}
}