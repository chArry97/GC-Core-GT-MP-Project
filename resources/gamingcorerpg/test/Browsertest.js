var test_browser = null;

function testhtml(args) {
		var res = API.getScreenResolution();
		
		test_browser = API.createCefBrowser(res.Width/4, res.Height-20, true);
		API.waitUntilCefBrowserInit(test_browser);
		API.setCefBrowserPosition(test_browser, res.Width - res.Width/4, 10);
		API.loadPageCefBrowser(test_browser, "/web/charcreator.html");
		API.setHudVisible(true);
		API.setCanOpenChat(false);
		API.showCursor(true);
		API.waitUntilCefBrowserLoaded(test_browser);
}