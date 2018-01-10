using System;
using System.Data;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;

public class BikeRental : Script {
	
	public BikeRental() {
		API.onResourceStart += SpawnBikeRentalsOnServerStart;
	}
	
	public void SpawnBikeRentalsOnServerStart() {
		
	}
	
}