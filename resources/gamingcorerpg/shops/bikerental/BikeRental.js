
function sendRentBike(name) {
	API.triggerServerEvent("eventBikeRental", name);
}