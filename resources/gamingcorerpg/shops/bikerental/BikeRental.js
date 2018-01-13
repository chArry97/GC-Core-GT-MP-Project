var bikeshop_browser = null;

function sendRentBike(name) {
    if (name != null) {
        API.triggerServerEvent("eventBikeRental", name);
    }
    closeBikeShop();
}

function sendReturnBike(name) {
    if (name != null) {
        API.triggerServerEvent("eventBikeReturn");
    }
    closeBikeShop();
}

function closeBikeShop() {
    API.showCursor(false);
    API.destroyCefBrowser(bikeshop_browser);
    API.setCanOpenChat(true);
    API.setHudVisible(true);
}

API.onServerEventTrigger.connect(function(eventname, args) {
    if (eventname === "bikeShopOpen") {
        var res = API.getScreenResolution();

        bikeshop_browser = API.createCefBrowser(res.Width / 2, res.Height / 1.5, true);
        API.waitUntilCefBrowserInit(bikeshop_browser);
        API.setCefBrowserPosition(bikeshop_browser, res.Width / 4, res.Height / 6);
        API.loadPageCefBrowser(bikeshop_browser, "/web/summerbike.html");
        API.setHudVisible(true);
        API.setCanOpenChat(false);
        API.showCursor(true);
        API.waitUntilCefBrowserLoaded(bikeshop_browser);
    } else if (eventname === "bikeReturnOpen") {
        var res = API.getScreenResolution();

        bikeshop_browser = API.createCefBrowser(res.Width / 3, res.Height / 3, true);
        API.waitUntilCefBrowserInit(bikeshop_browser);
        API.setCefBrowserPosition(bikeshop_browser, res.Width / 3, res.Height / 3);
        API.loadPageCefBrowser(bikeshop_browser, "/web/summerbikereturn.html");
        API.setHudVisible(true);
        API.setCanOpenChat(false);
        API.showCursor(true);
        API.waitUntilCefBrowserLoaded(bikeshop_browser);
    }
});