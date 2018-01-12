using System;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;

public class AdminCommands : Script
{
    [Command("tp", "Usage: /tp *Player*", Alias = "teleport", GreedyArg = true)]
    public void cmd_teleport(Client sender, Client target)
    {
		if (API.getEntityData(sender.handle, "Adminlevel") >= 5) {
			API.sendNotificationToPlayer(sender, "~w~Du wurdest zum Spieler teleportiert: ~r~" + target.name);
			API.sendNotificationToPlayer(target, "~r~" + sender.name + " ~w~hat sich zu dir teleportiert!");
			sender.position = target.position;
		} else {
			API.sendNotificationToPlayer(sender, "~w~Du hast nicht genügend Rechte, um diesen Befehl zu benutzen");
		}
    }
	
    [Command("tphere", "Usage: /tphere *Player*", Alias = "teleportHere", GreedyArg = true)]
    public void cmd_teleportHere(Client sender, Client target)
    {
		if (API.getEntityData(sender.handle, "Adminlevel") >= 5) {
			API.sendNotificationToPlayer(sender, "~w~Du teleportierst den Spieler zu dir:~r~" + sender.name);
			API.sendNotificationToPlayer(target, "~r~" + target.name + " ~w~Du wurdest teleportiert!");
			target.position = sender.position;
		} else {
			API.sendNotificationToPlayer(sender, "~w~Du hast nicht genügend Rechte, um diesen Befehl zu benutzen");
		}
    }
	
    [Command("tppos", "Usage: /tppos *X* *Y* *Z*", Alias = "teleportToPosition")]
    public void cmd_teleportPosition(Client sender, float x, float y, float z)
    {
		if (API.getEntityData(sender.handle, "Adminlevel") >= 5) {
			API.sendNotificationToPlayer(sender, "~w~Du wurdest teleportiert!");
			sender.position = new Vector3((float) x, (float) y, (float) z);
		} else {
			API.sendNotificationToPlayer(sender, "~w~Du hast nicht genügend Rechte, um diesen Befehl zu benutzen");
		}
    }
	
    [Command("whatpos", "Usage: /whatpos", Alias = "whatismyposition")]
    public void cmd_whatIsMyPosition(Client sender)
    {
		if (API.getEntityData(sender.handle, "Adminlevel") >= 3) {
			Vector3 position = sender.position;
			API.sendChatMessageToPlayer(sender, "~w~Deine aktuelle Position ist: " + position.X + " " + position.Y + " " + position.Z);
		} else {
			API.sendNotificationToPlayer(sender, "~w~Du hast nicht genügend Rechte, um diesen Befehl zu benutzen");
		}
    }
	
    [Command("adminlvl", "Usage: /adminlvl *Player(SocialClubName)* *Adminlevel*", Alias = "setadminlevel")]
    public void cmd_setAdminlevel(Client sender, Client target, int level)
    {
		if (API.getEntityData(sender.handle, "Adminlevel") >= 6) {
			
			MySqlConnection conn = Database.getDatabase();
			try {
				conn.Open();
				
				MySqlCommand cmd = conn.CreateCommand();
				cmd = conn.CreateCommand();
				cmd.CommandText = "UPDATE user SET Adminlevel = @adminlevel WHERE SocialClubName = @socialclubname";
				cmd.Parameters.AddWithValue("@adminlevel", level.ToString());
				cmd.Parameters.AddWithValue("@socialclubname", target.socialClubName);

				cmd.ExecuteNonQuery();
				conn.Close();
				
				API.setEntityData(target.handle, "Adminlevel", level);
				API.sendNotificationToPlayer(sender, "~w~Du hast ~r~" + target.name + " ~w~zu Adminlevel ~r~" + level.ToString() + " ~w~hinzugefügt");
				API.sendNotificationToPlayer(target, "~w~Du wurdest von ~r~" + sender.name + " ~w~zu Adminlevel ~r~" + level.ToString() + " ~w~hinzugefügt");
			} catch (MySqlException) {
				API.consoleOutput ("ERROR connecting to database failed");
				sender.sendNotification ("Register Error", "Es ist ein Fehler aufgetreten!");
			}
		} else {
			API.sendNotificationToPlayer(sender, "~w~Du hast nicht genügend Rechte, um diesen Befehl zu benutzen");
		}
    }
}
