var bikeshop_browser = null;

function sendRentBike(name) {
	API.triggerServerEvent("eventBikeRental", name);
	
	API.showCursor(false);
	API.destroyCefBrowser(bikeshop_browser);
	API.setCanOpenChat(true);      
	API.setHudVisible(true);
}

API.onServerEventTrigger.connect(function(eventname, args) {
	if (eventname === "bikeShopOpen") {
		var res = API.getScreenResolution();
	
		bikeshop_browser = API.createCefBrowser(res.Width/2, res.Height/1.5, true);
		API.waitUntilCefBrowserInit(bikeshop_browser);
		API.setCefBrowserPosition(bikeshop_browser, res.Width/4, res.Height/6);
		API.loadPageCefBrowser(bikeshop_browser, "/web/summerbike.html");
		API.setHudVisible(true);
		API.setCanOpenChat(false);
		API.showCursor(true);
		API.waitUntilCefBrowserLoaded(bikeshop_browser);
	}
});