var model = {
    EnterTheGame: function () {
        console.log("2");
        location.pathname = "Gameplay/id=" + window.currentGameId;
    },
    ApplicationTeam: function () {
        $.get('/Administration/ManagementTeamPlay/Application', { gameId: window.currentGameId })
        .done(function () {
            location.reload();
        })
        .fail(function () {
            notify.error("Не удалось выполнить заявку");
        });
    }
}

var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel, $('#page-content').get(0));
}

function ViewModel() {
    this.application = function () {
        model.ApplicationTeam();
    }

    this.enterTheGame = function () {
        model.EnterTheGame();
    }
}
