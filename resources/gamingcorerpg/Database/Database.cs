using System;
using MySQL.Data.MySqlClient;
using GTANetworkServer;

class Database : Script {
	
	private string server = "";
	private string database = "";
	private string loginName = "";
	private string password = "";
	
	private static string connString = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + loginName + ";PASSWORD=" + password + ";";
	private static MySqlConnection connection;
	private static MySqlCommand command;
	private static MySqlDataReader reader;
	
	public static MySqlDataReader executeCommand(string command) {
		connection = new MySqlConnection(connString);
		command = connection.createCommand();
		command.CommandText = command;
		connection.Open();
		
		return reader = command.ExecuteReader();
	}
	
	public static void endCommand() {
		connection.Close();
	}
}