using System;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;

public class Browsertest : Script
{
    [Command("html", "Usage: /testhtml", Alias = "testhtml")]
    public void cmd_testhtml(Client sender)
    {
		API.triggerClientEvent(sender, "testhtml");
    }
}