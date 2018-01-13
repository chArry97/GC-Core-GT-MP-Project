using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using MySql.Data.MySqlClient;

public class GamingcoreRpg : Script {

    public GamingcoreRpg()
    {
		API.onResourceStart += OnServerStart;
    }
	
	public void OnServerStart() {
		API.consoleOutput ("Server starting...");
		MySqlConnection conn = Database.getDatabase();
		try {
			conn.Open();
			API.consoleOutput ("Connected to Database");
			conn.Close();
			API.consoleOutput ("Connection to Database closed");
		} catch (MySqlException) {
			API.consoleOutput ("ERROR connecting to database failed");
		}	
	}
}