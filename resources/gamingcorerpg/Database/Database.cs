using System;
using MySql.Data.MySqlClient;

class Database{
	
	public static MySqlConnection getDatabase() {
		XmlDocument databaseConfig = ConfigurationLoader.getConfigurationLoader().getConfig("database");
		string host = databaseConfig.GetElementsByTagName("host")[0].InnerText;
		string user = databaseConfig.GetElementsByTagName("user")[0].InnerText;
		string password = databaseConfig.GetElementsByTagName("password")[0].InnerText;
		string database = databaseConfig.GetElementsByTagName("database")[0].InnerText;

		string connectionData = "server=" + host + ";uid=" + user + ";" + "pwd=" + password + ";database=" +
									database + ";Allow Zero Datetime=true;";
		return new MySqlConnection(myConnectionString);
	}
	
}