var model = {
    createCommand: function (name) {
        if (name == null) {
            alert("Введите название команды");
            return;
        }

        $.get("/User/Command/CreateTeam/", { Name: name })
            .done(function () {
                // TODO Убрать alert
                alert("Ok");
                location.reload();
            })
            .fail(function () {
                alert("Не удалось создать команду");
            });
    }
}

var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel);
}

function ViewModel() {
    this.inputNewCommandName = ko.observable();

    this.createCommand = function () {
        model.createCommand(this.inputNewCommandName());
    }
}
