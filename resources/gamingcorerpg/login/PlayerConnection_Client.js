var login_browser = null;

API.onResourceStart.connect(function() {
	var res = API.getScreenResolution();
	
	login_browser = API.createCefBrowser(res.Width/2, res.Height/2);
	API.waitUntilCefBrowserInit(login_browser);
	API.setCefBrowserPosition(login_browser, res.Width/4, res.Height/4);
	API.loadPageCefBrowser(login_browser, "/web/start.html");
	API.setHudVisible(false);
	API.setCanOpenChat(false);
	API.showCursor(true);
    API.waitUntilCefBrowserLoaded(browser);
});

API.onServerEventTrigger.connect(function(eventname, args) {
	if (eventname == "spawnPlayer") {
		API.showCursor(false);
		API.destroyCefBrowser(login_browser);
		API.setCanOpenChat(true);      
		API.setHudVisible(true);
	}
});

function loginPlayer(String email, String password) {
	API.triggerServerEvent("eventClientLogin", email, password);
}

function registerPlayer(String email, String password) {
	API.triggerServerEvent("eventClientRegister", email, password);
}