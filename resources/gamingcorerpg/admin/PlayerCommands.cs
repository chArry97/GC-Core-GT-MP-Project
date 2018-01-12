using System;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;

public class PlayerCommands : Script
{
    [Command("money")]
    public void cmd_showMoney(Client sender)
    {
		API.sendChatMessageToPlayer(sender, "~w~Du besitzt ~r~" + API.getEntitySyncedData(sender, "Wallet")
				+ "$ ~w~in der Tasche und ~r~" + API.getEntitySyncedData(sender, "Bank")
				+ "$ ~w~auf der Bank.");
    }
}
