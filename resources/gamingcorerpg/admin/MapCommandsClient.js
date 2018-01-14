function createMapMainMenu() {

    var mapMainMenu = API.createMenu("Map Menu", "Aktion", 0, 0, 6);
    mapMainMenu.AddItem(API.createMenuItem("Hinzufügen", "Neues Element hinzufügen"));
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
    var mapAddMenu = API.createMenu("Typ", "Wähle das Element.", 0, 0, 6);
    mapAddMenu.AddItem(API.createMenuItem("Bike Shop", "Bike Shop hinzufügen"));
    mapAddMenu.AddItem(API.createMenuItem("xxxxx", "xxxxx"));
    mapAddMenu.Visible = true;

    mapAddMenu.OnItemSelect.connect(() => {
        mapAddMenu.Visible = false;
        mapAddMenu = undefined;
        createMapAddMenu();
    });
}