API.onServerEventTrigger.connect(function (eventName, args) {
    switch (eventName) {
        case 'spawnPlayer':
            resource.PlayerConnection_Client.spawnPlayer(args);
            break;
        case 'IndicatorSubtitle':
            resource.VehIndicator.IndicatorSubtitle(args);
            break;
        case 'testhtml':
            resource.Browsertest.testhtml(args);
            break;
        case 'createMapMenu':
            resource.MapCommandsClient.createMapMainMenu();
            break;
    }
});