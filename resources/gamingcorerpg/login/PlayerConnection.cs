using System;
using System.Data;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
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
		} else if (eventName.Equals("eventClientRegister")) {
            int registerResult = createAccount(player.name, arguments[1].ToString(), arguments[0].ToString(), player.socialClubName);
            switch (registerResult) {
                case 0:
                    loginPlayerSuccess(player, (string)arguments[0]);
                    break;
                case 1:
                    player.sendNotification("Register Error", "Es ist ein Fehler aufgetreten!");
                    break;
                case 2:
                    player.sendNotification("Register Error", "Ein Account mit dieser E-Mail Adresse ist schon vorhanden!");
                    break;
                case 3:
                    player.sendNotification("Register Error", "Die eingegebene E-Mail Adresse ist ungültig!");
                    break;

                }
        }
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
        player.position = new Vector3(-1404.072, 53.73562, 53.04605);
        //player.position = new Vector3(1975.552, 3820.538, 33.44833);
		player.transparency = 255;
		player.freeze (false);
		player.collisionless = false;
		player.triggerEvent ("spawnPlayer");
	}

    /// <summary>
    /// Creates Account in DB with salted and encrypted PW
    /// </summary>
    /// <param name="username">username</param>
    /// <param name="password">password</param>
    /// <param name="email">email</param>
    /// <param name="socialClubName">socialClubName</param>
    /// <returns>int -> resultcode</returns>
    public int createAccount(string username, string password, string email, string socialClubName)
    {
        String saltedPassword = PasswordDerivation.Derive(password);
        
        Regex reg = new Regex(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                                + "@"
                                + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$");
        if (!reg.IsMatch(email))
            return 3;



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

                command.CommandText = "INSERT INTO user (Username, SocialClubName, EMail, Password) Values (@username, @socialclubname, @email, @password)";
                command.Prepare();

                if (command.ExecuteNonQuery() > 0)
                    result = 0;
                else
                    result = 1;
                
            } else
                result = 1;
            conn.Close();
        }
        catch (Exception err) {
            Console.WriteLine(err);
            result = 2;
        }
        return result;
    }

    /// <summary>
    /// Checks the submitted login data with the stored salted/encrypted password.
    /// </summary>
    /// <param name="email">email</param>
    /// <param name="password">password</param>
    /// <returns>int -> resultcode</returns>
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