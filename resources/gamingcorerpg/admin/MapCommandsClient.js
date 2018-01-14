function createMapMainMenu() {

    var mapMainMenu = API.createMenu("Map Menu", "Aktion", 0, 0, 6);
    mapMainMenu.AddItem(API.createMenuItem("Hinzuf�gen", "Neues Element hinzuf�gen"));
    mapMainMenu.AddItem(API.createMenuItem("Entfernen", "Element entfernen (TBD)"));
    mapMainMenu.Visible = true;

    mapMainMenu.OnItemSelect.connect(() => {
        mapMainMenu.Visible = false;
        mapMainMenu = undefined;
        createMapAddMenu();
    });


    //API.sendChatMessage("You selected:  ~g~" + item.Text + " " + index);

}

function createMapAddMenu() {
    var mapAddMenu = API.createMenu("Typ", "W�hle das Element.", 0, 0, 6);
    mapAddMenu.AddItem(API.createMenuItem("Bike Shop", "Bike Shop hinzuf�gen"));
    mapAddMenu.AddItem(API.createMenuItem("xxxxx", "xxxxx"));
    mapAddMenu.Visible = true;

    mapAddMenu.OnItemSelect.connect(() => {
        mapAddMenu.Visible = false;
        mapAddMenu = undefined;
        createMapAddMenu();
    });
}