using System;
using System.Data;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;

public class PlayerConnection : Script {
	
	public PlayerConnection() {
		API.onPlayerConnected += OnPlayerConnectedEvent;
		API.onClientEventTrigger += OnClientEvent;
	}
	
	public void OnPlayerConnectedEvent(Client player) {
		player.nametagVisible = false;
		player.invincible = true;
		player.position = new Vector3(0, 0, 200);
		player.transparency = 0;
		player.freeze (true);
		player.collisionless = true;
	}
	
	public void OnClientEvent(Client player, string eventName, params object[] arguments) {
		if (eventName.Equals("eventClientLogin")) {
			MySqlConnection conn = Database.getDatabase();
			try {
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = "SELECT count(EMail) FROM user WHERE EMail = @email";
				cmd.Parameters.AddWithValue("@email", arguments[0]);
				
				Int32 accounts = Int32.Parse(cmd.ExecuteScalar().ToString());
				
				if (accounts > 0) {
					cmd = conn.CreateCommand();
					cmd.CommandText = "SELECT Password FROM user WHERE Email = @email";
					cmd.Parameters.AddWithValue("@email", arguments[0]);
					
					DataTable result = new DataTable();

					result.Load(cmd.ExecuteReader());
					
					string dbpw = (string) result.Rows[0]["Password"];
					
					if (arguments[1].Equals(PlayerConnection.Base64Decode(dbpw))) {
						player.sendNotification ("Login", "Einloggen erfolgreich");
						loginPlayerSuccess(player, (string) arguments[0]);
					} else {
						player.sendNotification ("Login Error", "Email oder Passwort falsch");
					}
				} else {
					player.sendNotification ("Login Error", "Diese Email ist uns nicht bekannt");
				}
				conn.Close();
			} catch (MySqlException) {
				API.consoleOutput ("ERROR connecting to database failed");
				player.sendNotification ("Login Error", "Es ist ein Fehler aufgetreten!");
			}	
		} else if (eventName.Equals("eventClientRegister")) {
			MySqlConnection conn = Database.getDatabase();
			try {
				conn.Open();
				MySqlCommand cmd = conn.CreateCommand();
				cmd.CommandText = "SELECT count(EMail) FROM user WHERE EMail = @email";
				cmd.Parameters.AddWithValue("@email", arguments[0]);
				
				Int32 accounts = Int32.Parse(cmd.ExecuteScalar().ToString());
				
				if (accounts == 0) {
					cmd = conn.CreateCommand();
					cmd.CommandText = "INSERT INTO user (Username, SocialClubName, EMail, Password) Values (@username, @socialclubname, @email, @password)";
					cmd.Parameters.AddWithValue("@username", player.name);
					cmd.Parameters.AddWithValue("@socialclubname", player.socialClubName);
					cmd.Parameters.AddWithValue("@email", arguments[0]);
					cmd.Parameters.AddWithValue("@password", PlayerConnection.Base64Encode((string) arguments[1]));

					cmd.ExecuteNonQuery();
					loginPlayerSuccess(player, (string) arguments[0]);
				} else {
					player.sendNotification ("Register Error", "Ein Account mit dieser Email ist schon vorhanden!");
				}
				conn.Close();
			} catch (MySqlException) {
				API.consoleOutput ("ERROR connecting to database failed");
				player.sendNotification ("Register Error", "Es ist ein Fehler aufgetreten!");
			}
		}
	}
	
	private static string Base64Encode(string plainText) {
		var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
		return System.Convert.ToBase64String(plainTextBytes);
	}


	private static string Base64Decode(string base64EncodedData) {
		var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
		return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
	}
	
	private void loginPlayerSuccess(Client player, string email) {
		
		MySqlConnection conn = Database.getDatabase();
		try {
			conn.Open();
			MySqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = "SELECT Adminlevel FROM user WHERE EMail = @email";
			cmd.Parameters.AddWithValue("@email", email);
			
			DataTable result = new DataTable();
			result.Load(cmd.ExecuteReader());
			
			API.setEntityData(player.handle, "Adminlevel", result.Rows[0]["Adminlevel"]);
			
			conn.Close();
		
			MoneyHandler.loadMoney(player);
		} catch (MySqlException) {
			API.consoleOutput ("ERROR connecting to database failed");
			player.sendNotification ("Login Error", "Informationen über Gruppenstatus konnte nicht abgerufen werden!");
		}
		
		player.nametagVisible = true;
		player.invincible = false;
		//player.position = new Vector3(-1370.6250, 56.1227, 0);
		player.position = new Vector3(1975.552, 3820.538, 33.44833);
		player.transparency = 255;
		player.freeze (false);
		player.collisionless = false;
		player.triggerEvent ("spawnPlayer");
	}
	
}