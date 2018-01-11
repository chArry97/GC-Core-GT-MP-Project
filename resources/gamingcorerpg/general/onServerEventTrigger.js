API.onServerEventTrigger.connect(function (eventName, args) {
    switch (eventName) {
        case 'testhtml':
            resource.Browsertest.testhtml();
            break;
    }
});