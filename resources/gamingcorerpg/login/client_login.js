var login_browser = null;

API.onResourceStart.connect(function() {
	var res = API.getScreenResolution();
	
	login_browser = API.createCefBrowser(res.Width/2, res.Height/2);
	API.waitUntilCefBrowserInit(login_browser);
	API.setCefBrowserPosition(login_browser, res.Width/2, res.Height/2);
	API.loadPageCefBrowser(login_browser, "/web/start.html");
	API.setBrowserHeadless(true);
	API.showCursor(true);
	API.setCanOpenChat(false);
});

function close() {
	API.showCursor(false);
	API.destroyCefBrowser(login_browser);
	API.setCanOpenChat(true);
}