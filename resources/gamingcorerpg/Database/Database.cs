using System;
using MySQL.Data.MySqlClient;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;

namespace db {
	class Database{
		
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
}