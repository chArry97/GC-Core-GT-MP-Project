using System;
using GrandTheftMultiplayer.Server;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Constant;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;
using GrandTheftMultiplayer.Shared.Math;

public class Playercommandlist
{
    [Command ("hilfe")]
	public void hilfe(Client player)
	{
        API.sendChatMessageToPlayer(player, "~r~System:~w~ Folgende Befehle sind auf diesem Server Aktiv:");
        API.sendChatMessageToPlayer(player, "~r~System:~w~ /veh /vehh /repair /tp /tptome");
    }
}
