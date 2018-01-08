using System;
using System.Data;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;

public class SummerBike {
	
	private readonly int pedHash = 1206185632;
	
	private CylinderColShape col;
	private NetHandle ped;
	
	public SummerBike(Vector3 colPos, Vector3 sellerPos, float heading) {
		initiateBikeRental(colPos, sellerPos, heading);
		
		API.shared.onResourceStop += stopBikeRental;
	}
	
	private void initiateBikeRental(Vector3 colPos, Vector3 sellerPos, float heading) {
		col = API.shared.createCylinderColShape(colPos, 1.0f, 1.0f);
		col.onEntityEnterColShape += onBikeRentalTriggerEnter;
		
		ped = API.shared.createPed(pedHash, sellerPos, heading);
	}
	
	private void onBikeRentalTriggerEnter(ColShape shape, NetHandle entity) {
		
		Client player = API.getPlayerFromHandle(entity);
		if (player == null) {
			return;
		}
		//SHOP THINGS
		player.sendNotification("SummerBike Shop", "Wenn das hier fertig ist, kannst du hier Fahrräder leihen");
	}
	
	private void stopBikeRental() {
		API.shared.deleteEntity(ped);
	}
}