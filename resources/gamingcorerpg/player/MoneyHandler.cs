using System;
using System.Data;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;

public class MoneyHandler {
	
	public static bool loadMoney(Client player) {
		MySqlConnection conn = Database.getDatabase();
		try {
			conn.Open();
			MySqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = "SELECT Wallet, Bank FROM user WHERE SocialClubName = @socialclubname";
			cmd.Parameters.AddWithValue("@socialclubname", player.socialClubName);
			
			DataTable result = new DataTable();
			result.Load(cmd.ExecuteReader());
			
			API.shared.setEntitySyncedData(player.handle, "Wallet", result.Rows[0]["Wallet"]);
			API.shared.setEntitySyncedData(player.handle, "Bank", result.Rows[0]["Bank"]);
			
			conn.Close();
			return true;
		} catch (MySqlException) {
			API.shared.consoleOutput ("ERROR connecting to database failed");
			return false;
		}
	}
	
	public static bool addToWallet(Client player, float amount) {
		MySqlConnection conn = Database.getDatabase();
		
		try {
			conn.Open();
			
			MySqlCommand cmd = conn.CreateCommand();
			cmd = conn.CreateCommand();
			cmd.CommandText = "UPDATE user SET Wallet = @wallet WHERE SocialClubName = @socialclubname";
			cmd.Parameters.AddWithValue("@wallet", (API.shared.getEntityData(player.handle, "Wallet") + amount).ToString());
			cmd.Parameters.AddWithValue("@socialclubname", player.socialClubName);

			cmd.ExecuteNonQuery();
			conn.Close();
				
			loadMoney(player);
			return true;
		} catch (MySqlException) {
			API.shared.consoleOutput ("ERROR connecting to database failed");
			return false;
		}
	}
	
	public static bool removeFromWallet(Client player, float amount) {
		MySqlConnection conn = Database.getDatabase();
		
		try {
			conn.Open();
			
			MySqlCommand cmd = conn.CreateCommand();
			cmd = conn.CreateCommand();
			cmd.CommandText = "UPDATE user SET Wallet = @wallet WHERE SocialClubName = @socialclubname";
			cmd.Parameters.AddWithValue("@wallet", (API.shared.getEntityData(player.handle, "Wallet") - amount).ToString());
			cmd.Parameters.AddWithValue("@socialclubname", player.socialClubName);

			cmd.ExecuteNonQuery();
			conn.Close();
				
			loadMoney(player);
			return true;
		} catch (MySqlException) {
			API.shared.consoleOutput ("ERROR connecting to database failed");
			return false;
		}
	}
	
	public static bool addToBank(Client player, float amount) {
		MySqlConnection conn = Database.getDatabase();
		
		try {
			conn.Open();
			
			MySqlCommand cmd = conn.CreateCommand();
			cmd = conn.CreateCommand();
			cmd.CommandText = "UPDATE user SET Bank = @bank WHERE SocialClubName = @socialclubname";
			cmd.Parameters.AddWithValue("@bank", (API.shared.getEntityData(player.handle, "Bank") + amount).ToString());
			cmd.Parameters.AddWithValue("@socialclubname", player.socialClubName);

			cmd.ExecuteNonQuery();
			conn.Close();
				
			loadMoney(player);
			return true;
		} catch (MySqlException) {
			API.shared.consoleOutput ("ERROR connecting to database failed");
			return false;
		}
	}

	public static bool removeFromBank(Client player, float amount) {
		MySqlConnection conn = Database.getDatabase();
		
		try {
			conn.Open();
			
			MySqlCommand cmd = conn.CreateCommand();
			cmd = conn.CreateCommand();
			cmd.CommandText = "UPDATE user SET Bank = @bank WHERE SocialClubName = @socialclubname";
			cmd.Parameters.AddWithValue("@bank", (API.shared.getEntityData(player.handle, "Bank") - amount).ToString());
			cmd.Parameters.AddWithValue("@socialclubname", player.socialClubName);

			cmd.ExecuteNonQuery();
			conn.Close();
				
			loadMoney(player);
			return true;
		} catch (MySqlException) {
			API.shared.consoleOutput ("ERROR connecting to database failed");
			return false;
		}
	}
	
	public static bool fromBankToWallet(Client player, float amount) {
		return (removeFromBank(player, amount) && addToWallet(player, amount));
	}
	
	public static bool fromWalletToBank(Client player, float amount) {
		return (removeFromWallet(player, amount) && addToBank(player, amount));
	}
}