var model = {
    EnterTheGame: function () {
        console.log("2");
        location.pathname = "Gameplay/" + window.currentGameId;
    }
}

var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel);
}

function ViewModel() {

    this.enterTheGame = function () {
        model.EnterTheGame();
        console.log("1");
    }
}
