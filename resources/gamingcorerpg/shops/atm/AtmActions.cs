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
        if (eventName == "atmDeposit")
        {
            float amount = float.Parse((string)arguments[0]);
            if (amount > 0 || amount < 10000000)
            {
                if (API.getEntitySyncedData(player, "Wallet") < amount)
                    arguments[0] = API.getEntitySyncedData(player, "Wallet");
                MoneyHandler.fromWalletToBank(player, amount);
                API.sendChatMessageToPlayer(player, "~w~Du hast ~r~" + amount + "$ ~w~auf der Bank platziert.");
                API.triggerClientEvent(player, "atmUpdateMoney");
            }
            else
            {
                API.sendNotificationToPlayer(player, "Der Betrag muss größer als 0 und kleiner als 10 Mio. sein!");
            }
        }
        else if (eventName == "atmPayOff")
        {
            float amount = float.Parse((string)arguments[0]);
            if (amount > 0 || amount < 10000000)
            {
                if (API.getEntitySyncedData(player, "Bank") < amount)
                    arguments[0] = API.getEntitySyncedData(player, "Bank");
                MoneyHandler.fromBankToWallet(player, amount);
                API.sendChatMessageToPlayer(player, "~w~Du hast ~r~" + amount + "$ ~w~von deinem Konto abgehoben.");
                API.triggerClientEvent(player, "atmUpdateMoney");
            }
            else
            {
                API.sendNotificationToPlayer(player, "Der Betrag muss größer als 0 und kleiner als 10 Mio. sein!");
            }
        }
    }

    [Command("createAtm")]
    public void cmd_createAtm(Client sender)
    {
        if (API.getEntityData(sender.handle, "Adminlevel") >= 5)
        {

            MySqlConnection conn = Database.getDatabase();

            try
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO world_objects (type, posX, posY, posZ, rotZ) VALUES (@type, @posX, @posY, @posZ, @rotZ)";
                cmd.Parameters.AddWithValue("@type", "atm");
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

            new Atm(sender.position, sender.rotation.Z);

            API.sendNotificationToPlayer(sender, "~w~Ein neuer Atm wurde erstellt");
        }
        else
        {
            API.sendNotificationToPlayer(sender, "~w~Du hast nicht genügend Rechte, um diesen Befehl zu benutzen");
        }
    }
}
