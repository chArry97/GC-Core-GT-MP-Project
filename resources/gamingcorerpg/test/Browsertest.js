var login_browser = null;

API.onServerEventTrigger.connect(function() {
	if (eventname === "testhtml") {
		var res = API.getScreenResolution();
		
		login_browser = API.createCefBrowser(res.Width/4, res.Height/2, true);
		API.waitUntilCefBrowserInit(login_browser);
		API.setCefBrowserPosition(login_browser, res.Width - res.Width/4, res.Height/4);
		API.loadPageCefBrowser(login_browser, "/web/charcreator.html");
		API.setHudVisible(true);
		API.setCanOpenChat(false);
		API.showCursor(true);
		API.waitUntilCefBrowserLoaded(login_browser);
	}
});