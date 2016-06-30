var model = {
    addNewPlayer: function (login) {
        if (login == null) {
            notify.error("Введите логин");
            return;
        }

        $.get("/User/Command/AddPlayer/", { Login: login })
            .done(function () {
                // TODO Убрать notify
                notify.success("Ok");
                location.reload();
            })
            .fail(function () {
                notify.error("Не удалось добавить игрока");
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
