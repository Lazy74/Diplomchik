var model = {
    createGame: function () {

        $.get("/Administration/CreateGame/")
            .done(function (data) {
                location.href = "/Administration/EditGameInformation/id=" + data;
            })
            .fail(function () {
                notify.error("Не удалось создать игру");
            });
    }
}

var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel, $('#page-content').get(0));
}

function ViewModel() {
    //this.inputNewCommandName = ko.observable();

    this.createGame = function () {
        model.createGame();
    }
}
