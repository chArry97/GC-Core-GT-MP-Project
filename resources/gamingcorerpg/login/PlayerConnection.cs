using System;
using System.Data;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;
//using gamingcorerpg.login;

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
			//MySqlConnection conn = Database.getDatabase();
			//try {
                //conn.Open();
                //MySqlCommand cmd = conn.CreateCommand();
            int resultLogin = loginAccount(arguments[0].ToString(), arguments[1].ToString());
            switch (resultLogin) {
                case 0:
                    loginPlayerSuccess(player, (string)arguments[0]);
                    break;
                case 1:
                    player.sendNotification("Login Error", "Email oder Passwort falsch");
                    break;
                case 2:
                    player.sendNotification("Login Error", "Es ist ein Fehler aufgetreten!");
                    break;
            }
                //loginPlayerSuccess(player, (string)arguments[0]);
    //            cmd.CommandText = "SELECT count(EMail) FROM user WHERE EMail = @email";
				//cmd.Parameters.AddWithValue("@email", arguments[0]);
				
				//Int32 accounts = Int32.Parse(cmd.ExecuteScalar().ToString());
				
				//if (accounts > 0) {
				//	cmd = conn.CreateCommand();
				//	cmd.CommandText = "SELECT Password FROM user WHERE Email = @email";
				//	cmd.Parameters.AddWithValue("@email", arguments[0]);
					
				//	DataTable result = new DataTable();

				//	result.Load(cmd.ExecuteReader());
					
				//	string dbpw = (string) result.Rows[0]["Password"];
					
				//	if (arguments[1].Equals(PlayerConnection.Base64Decode(dbpw))) {
				//		player.sendNotification ("Login", "Einloggen erfolgreich");
				//		loginPlayerSuccess(player, (string) arguments[0]);
				//	} else {
				//		player.sendNotification ("Login Error", "Email oder Passwort falsch");
				//	}
				//} else {
				//	player.sendNotification ("Login Error", "Diese Email ist uns nicht bekannt");
				//}
				//conn.Close();
			//} catch (MySqlException) {
				//API.consoleOutput ("ERROR connecting to database failed");
				//player.sendNotification ("Login Error", "Es ist ein Fehler aufgetreten!");
			//}	
		} else if (eventName.Equals("eventClientRegister")) {

            //MySqlConnection conn = Database.getDatabase();
            //try
            //{
            //conn.Open();
            //MySqlCommand cmd = conn.CreateCommand();
            //int registerResult = createAccount(player.name, (string)arguments[1], (string)arguments[0], player.socialClubName);
            int registerResult = createAccount(player.name, arguments[1].ToString(), arguments[0].ToString(), player.socialClubName);
            switch (registerResult)
                {
                    case 0:
                        loginPlayerSuccess(player, (string)arguments[0]);
                        break;
                    case 1:
                        player.sendNotification("Register Error", "Es ist ein Fehler aufgetreten!");
                        break;
                    case 2:
                        player.sendNotification("Register Error", "Ein Account mit dieser Email ist schon vorhanden!");
                        break;

                }

                //cmd.CommandText = "SELECT count(EMail) FROM user WHERE EMail = @email";
                //cmd.Parameters.AddWithValue("@email", arguments[0]);

                //Int32 accounts = Int32.Parse(cmd.ExecuteScalar().ToString());

                //if (accounts == 0) {
                //cmd = conn.CreateCommand();
                //cmd.CommandText = "INSERT INTO user (Username, SocialClubName, EMail, Password) Values (@username, @socialclubname, @email, @password)";
                //md.Parameters.AddWithValue("@username", player.name);
                //cmd.Parameters.AddWithValue("@socialclubname", player.socialClubName);
                //cmd.Parameters.AddWithValue("@email", arguments[0]);
                //cmd.Parameters.AddWithValue("@password", PlayerConnection.Base64Encode((string) arguments[1]));

                //cmd.ExecuteNonQuery();
					//loginPlayerSuccess(player, (string) arguments[0]);
				//} else {
				//	player.sendNotification ("Register Error", "Ein Account mit dieser Email ist schon vorhanden!");
				//}
				//conn.Close();
			//} catch (MySqlException) {
				//API.consoleOutput ("ERROR connecting to database failed");
				//player.sendNotification ("Register Error", "Es ist ein Fehler aufgetreten!");
			//}
        }
    }
	
	//private static string Base64Encode(string plainText) {
	//	var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
	//	return System.Convert.ToBase64String(plainTextBytes);
	//}


	//private static string Base64Decode(string base64EncodedData) {
	//	var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
	//	return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
	//}
	
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



    /////////////////////////////////////////////
    public int createAccount(string username, string password, string email, string socialClubName)
    {
        /*  Now we'll generate our salted password and we will just send it to the database

            Example: PasswordDerivation.Derive("bestPasswordEver");
            Output: prHRZBO/xCXJrRpgas1cUA==:10000:16:LqTubT/4KhJWR+qwogrZqw==
            This is our salted password, this is how we will see the users password into the database
        */
        String saltedPassword = PasswordDerivation.Derive(password);

        /* 
            Remember that these instances has to be from your Database Connection class
            Setup your MySQL database: https://wiki.gt-mp.net/index.php?title=MySql

            This will open a connection with your MySQL Database.
            After that we create a MySQLCommand to send our queries for your database.

            This simplifies a lot you code and avoid boilerplate.
        */
        int result;
        try
        {   
            MySqlConnection conn = Database.getDatabase();
            MySqlCommand command = conn.CreateCommand();
            //MySqlDataReader Reader;
            conn.Open();

            command.CommandText = "SELECT count(EMail) FROM user WHERE EMail = @email";
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", saltedPassword);
            command.Parameters.AddWithValue("@socialclubname", socialClubName);



            Int32 accounts = Int32.Parse(command.ExecuteScalar().ToString());
            
            if (accounts == 0)
            {
                // We use command parameters to avoid SQL Injections
                // Learn more about: https://dev.mysql.com/doc/connector-net/en/connector-net-programming-prepared-preparing.html
                //command.CommandText = "INSERT INTO AccountsTable (username, password) VALUES (@username, @password)";

                command.CommandText = "INSERT INTO user (Username, SocialClubName, EMail, Password) Values (@username, @socialclubname, @email, @password)";
                command.Prepare();


                /* 
                 * This will execute our query and will return a int value, that means the number of rows that has been affected
                */
                
                if (command.ExecuteNonQuery() > 0)
                    result = 0;
                else
                    result = 1;
                
            } else
            {
                result = 1;
                //player.sendNotification("Register Error", "Ein Account mit dieser Email ist schon vorhanden!");
            }
            conn.Close();
        }
        catch (Exception err) {
            Console.WriteLine(err);
            //player.sendNotification("Register Error", "Es ist ein Fehler aufgetreten!");
            result = 2;
        }
        return result;
    }

    /**
     * This method will connect into the database and return if user password matches, if yes, we can login
     * 
     * @param player is the client that we'll create an account
     * @param username input from our login form
     * @param password plain input password from our login form and isn't salted 
     *
     * @return Boolean return if password match or do not.
     */
    public static int loginAccount(string email, string password)
    {
        int result;
        try
        {
            MySqlConnection conn = Database.getDatabase();
            MySqlCommand command = conn.CreateCommand();
            conn.Open();

            command.CommandText = "SELECT password FROM user WHERE EMail=@email";
            command.Prepare();
            command.Parameters.AddWithValue("@email", email);

            //get salted PW from DB and check with submitted pw	
            string saltedPassword = (string)command.ExecuteScalar();
            if(saltedPassword != null && PasswordDerivation.Verify(saltedPassword, password)) 
                result = 0;
            else
                result = 1;

            conn.Close();
        }
        catch (Exception err) { Console.WriteLine(err); result = 2;}
        return result;
    }
}