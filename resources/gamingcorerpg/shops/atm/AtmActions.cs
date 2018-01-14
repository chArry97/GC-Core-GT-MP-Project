using System;
using System.Collections.Generic;
using System.Text;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;

class AtmActions : Script
{

    public AtmActions()
    {
        API.onClientEventTrigger += OnPlayerDoTransaction;
    }

    public void OnPlayerDoTransaction(Client player, string eventName, params object[] arguments)
    {
        if (eventName == "eventBikeRental")
        {
            float costs = float.MaxValue;
            float returnValue = float.MaxValue;
            switch ((string)arguments[0])
            {
                case "Cruiser":
                    costs = 60.0f;
                    returnValue = 40.0f;
                    break;
                case "Fixter":
                    costs = 90.0f;
                    returnValue = 60.0f;
                    break;
                case "Bmx":
                    costs = 120.0f;
                    returnValue = 80.0f;
                    break;
                default:
                    break;
            }

            if (API.getEntitySyncedData(player, "Wallet") > costs)
            {
                if (!MoneyHandler.removeFromWallet(player, costs))
                {
                    API.sendNotificationToPlayer(player, "Der Fahrradverleih hat dir das Fahrrad kostenlos zur Verfügung gestellt.");
                }

                VehicleHash vehicleHash = API.vehicleNameToModel((string)arguments[0]);
                if (vehicleHash != ((VehicleHash)0))
                {
                    Vector3 vehPos = player.position;
                    Vector3 vehRot = player.rotation;
                    NetHandle vehHandle = API.createVehicle(vehicleHash, vehPos, new Vector3(0, 0, vehRot.Z), 157, 157);
                    API.setEntityData(vehHandle, "Owner", player.socialClubName);
                    API.setEntityData(vehHandle, "Loaned", true);
                    API.setEntityData(vehHandle, "ReturnValue", returnValue);

                    API.sendNotificationToPlayer(player, "Du hast ein Fahrrad geliehen.");

                    API.setPlayerIntoVehicle(player, vehHandle, -1);
                }
            }
            else
            {
                API.sendNotificationToPlayer(player, "Du hast nicht genügend Geld im Portmonee.");
            }
        }
        else if (eventName == "eventBikeReturn")
        {
            if (player.isInVehicle)
                if (API.shared.hasEntityData(player.vehicle, "Loaned"))
                    if (API.shared.getEntityData(player.vehicle, "Loaned"))
                    {
                        float returnValue = API.getEntityData(player.vehicle, "ReturnValue");
                        MoneyHandler.addToWallet(player, returnValue);
                        API.deleteEntity(player.vehicle);
                        API.sendChatMessageToPlayer(player, "~w~Du hast ~r~" + returnValue + "$ ~w~für das Zurückgeben eines Fahrrads erhalten.");
                    }
        }
    }

    [Command("createBikeShop")]
    public void cmd_createBikeShop(Client sender)
    {
        if (API.getEntityData(sender.handle, "Adminlevel") >= 5)
        {

            MySqlConnection conn = Database.getDatabase();

            try
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO world_objects (type, posX, posY, posZ, rotZ) VALUES (@type, @posX, @posY, @posZ, @rotZ)";
                cmd.Parameters.AddWithValue("@type", "bikeshop");
                cmd.Parameters.AddWithValue("@posX", sender.position.X);
                cmd.Parameters.AddWithValue("@posY", sender.position.Y);
                cmd.Parameters.AddWithValue("@posZ", sender.position.Z);
                cmd.Parameters.AddWithValue("@rotZ", sender.rotation.Z);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (MySqlException)
            {
                API.shared.consoleOutput("ERROR connecting to database failed");
            }

            new SummerBikeShop(sender.position, sender.rotation.Z);

            API.sendNotificationToPlayer(sender, "~w~Ein neuer Bike Shop wurde erstellt");
        }
        else
        {
            API.sendNotificationToPlayer(sender, "~w~Du hast nicht genügend Rechte, um diesen Befehl zu benutzen");
        }
    }
}
