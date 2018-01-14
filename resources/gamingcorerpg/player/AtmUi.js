var bank_browser;

function sendPlayerMoney() {
    bank_browser.call("setBankMoney", API.getEntitySyncedData(API.getLocalPlayer(), "Bank"));
}

function closeBankUI() {
    API.showCursor(false);
    API.destroyCefBrowser(bank_browser);
    API.setCanOpenChat(true);
    API.setHudVisible(true);
}

API.onServerEventTrigger.connect(function (eventname, args) {
    if (eventname === "bankOpen") {
        var res = API.getScreenResolution();

        bank_browser = API.createCefBrowser(res.Width / 2, res.Height / 1.5, true);
        API.waitUntilCefBrowserInit(bank_browser);
        API.setCefBrowserPosition(bank_browser, res.Width / 4, res.Height / 6);
        API.loadPageCefBrowser(bank_browser, "/web/bank.html");
        API.setHudVisible(true);
        API.setCanOpenChat(false);
        API.showCursor(true);
        API.waitUntilCefBrowserLoaded(bank_browser);
    }
});

function openBankBrowser(args) {
    var res = API.getScreenResolution();

    bank_browser = API.createCefBrowser(res.Width / 2, res.Height / 2, true);
    API.waitUntilCefBrowserInit(bank_browser);
    API.setCefBrowserPosition(bank_browser, res.Width / 4, res.Height / 4);
    API.loadPageCefBrowser(bank_browser, "/web/bank.html");
    API.setHudVisible(true);
    API.setCanOpenChat(false);
    API.showCursor(true);
    API.waitUntilCefBrowserLoaded(bank_browser);
}
