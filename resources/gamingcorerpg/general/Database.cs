using System;
using System.Collections.Generic;
using System.Xml;
using MySql.Data.MySqlClient;

class Database{
	
    /// <summary>
    /// This method returns an already configured MySqlConnection object that can be used to communicate
    /// with the database. The database connection still needs to be opened and closed.
    /// </summary>
    /// <returns>Configured MySqlConnection Object</returns>
	public static MySqlConnection getDatabase() {
		XmlDocument databaseConfig = ConfigurationLoader.getConfigurationLoader().GetConfig("database");
		string host = databaseConfig.GetElementsByTagName("host")[0].InnerText;
		string user = databaseConfig.GetElementsByTagName("user")[0].InnerText;
		string password = databaseConfig.GetElementsByTagName("password")[0].InnerText;
		string database = databaseConfig.GetElementsByTagName("database")[0].InnerText;

		string connectionData = "server=" + host + ";uid=" + user + ";" + "pwd=" + password + ";database=" +
									database + ";Allow Zero Datetime=true;";
		return new MySqlConnection(connectionData);
	}
	
}