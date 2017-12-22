using System;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;

class Database{
	
	private static string server = "";
	private static string database = "";
	private static string loginName = "";
	private static string password = "";
	
	private static string connString = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + loginName + ";PASSWORD=" + password + ";";
	private static MySqlConnection connection;
	private static MySqlCommand command;
	private static MySqlDataReader reader;
	
	public static MySqlDataReader executeCommand(string commandText) {
		connection = new MySqlConnection(connString);
		command = connection.CreateCommand();
		command.CommandText = commandText;
		connection.Open();
		
		return reader = command.ExecuteReader();
	}
	
	public static void endCommand() {
		connection.Close();
	}
}