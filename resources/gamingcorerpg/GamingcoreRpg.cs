using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Constant;
using MySql.Data.MySqlClient;

public class GamingcoreRpg : Script {
    //private static API _api;

    //public static API api => _api;

    public GamingcoreRpg()
    {
        //_api = API;
		API.onResourceStart += OnServerStart;
    }
	
	public void OnServerStart() {
		API.consoleOutput ("Server starting...");
		MySqlConnection conn = Database.getDatabase();
		try {
			conn.Open();
			/*MySqlCommand cmd = conn.CreateCommand();
			cmd.CommandText = "INSERT INTO user (EMail, Password) VALUES ('Norman124', 'ganzgeheim')";
			
			cmd.ExecuteNonQuery();*/
			API.consoleOutput ("Connected to Database");
			conn.Close();
			API.consoleOutput ("Connection to Database closed");
		} catch (MySqlException) {
			API.consoleOutput ("ERROR connecting to database failed");
		}	
	}
}