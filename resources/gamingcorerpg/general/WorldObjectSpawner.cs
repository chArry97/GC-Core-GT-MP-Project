using System;
using System.Data;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;

public class WorldObjectSpawner : Script {
	
    public WorldObjectSpawner()
    {
        API.onResourceStart += LoadAndSpawnWorldObjects;
    }

    /// <summary>
    /// OnResourceStart method to call all methods that should spawn objects on Server Start
    /// </summary>
    public void LoadAndSpawnWorldObjects()
    {
        MySqlConnection conn = Database.getDatabase();

        try
        {
            conn.Open();

            LoadAndSpawnBikeShops(conn.CreateCommand());
            LoadAndSpawnAtms(conn.CreateCommand());
            conn.Close();
        }
        catch (MySqlException)
        {
            API.shared.consoleOutput("ERROR in Database");
        }
    }

    /// <summary>
    /// Loading all Bike Shops from the database and spawning them ingame
    /// </summary>
    /// <param name="cmd">Opened MySqlConnection</param>
    private void LoadAndSpawnBikeShops(MySqlCommand cmd)
    {
        cmd.CommandText = "SELECT posX, posY, posZ, rotZ FROM world_objects WHERE type = 'bikeshop'";

        DataTable result = new DataTable();
        result.Load(cmd.ExecuteReader());

        for(int i = 0; i < result.Rows.Count; i++)
        {
            Vector3 pos = new Vector3
            {
                X = (float) Convert.ToDouble(result.Rows[i]["posX"]),
                Y = (float) Convert.ToDouble(result.Rows[i]["posY"]),
                Z = (float) Convert.ToDouble(result.Rows[i]["posZ"])
            };
            float rotZ = (float) Convert.ToDouble(result.Rows[i]["rotZ"]);
            new SummerBikeShop(pos, rotZ);
        }
    }

    /// <summary>
    /// Loading all ATMs from the database and spawning them ingame
    /// </summary>
    /// <param name="cmd">Opened MySqlConnection</param>
    private void LoadAndSpawnAtms(MySqlCommand cmd)
    {
        cmd.CommandText = "SELECT posX, posY, posZ, rotZ FROM world_objects WHERE type = 'atm'";

        DataTable result = new DataTable();
        result.Load(cmd.ExecuteReader());

        for (int i = 0; i < result.Rows.Count; i++)
        {
            Vector3 pos = new Vector3
            {
                X = (float)Convert.ToDouble(result.Rows[i]["posX"]),
                Y = (float)Convert.ToDouble(result.Rows[i]["posY"]),
                Z = (float)Convert.ToDouble(result.Rows[i]["posZ"])
            };
            float rotZ = (float)Convert.ToDouble(result.Rows[i]["rotZ"]);
            new Atm(pos, rotZ);
        }
    }
}