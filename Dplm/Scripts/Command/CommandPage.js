var model = {
    addNewPlayer: function (login) {
        if (login == null) {
            alert("Введите логин");
            return;
        }

        $.get("/User/Command/AddPlayer/", { Login: login })
            .done(function () {
                // TODO Убрать alert
                alert("Ok");
                location.reload();
            })
            .fail(function () {
                alert("Не удалось добавить игрока");
            });
    }
}

var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel);
}

function ViewModel() {
    this.addNewPlayer = ko.observable();

    this.addPlayer = function() {
        model.addNewPlayer(this.addNewPlayer());
    }
}
