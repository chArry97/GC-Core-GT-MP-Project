using System;
using GrandTheftMultiplayer.Server.API;

public class PlayerConnection : Script {
	
	public PlayerConnection() {
		API.onPlayerConnected += OnPlayerConnectedEvent;
	}
	
	public void OnPlayerConnectedEvent(Client player) {
		player.nametagVisible = false;
		player.invincible = true;
		player.position = new Vector3(0, 0, 200);
		player.transparency = 0;
		player.freeze (true);
		player.collisionless = true;
	}
}