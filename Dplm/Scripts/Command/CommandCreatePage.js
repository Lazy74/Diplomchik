var model = {
    createCommand: function (name) {
        if (name == null) {
            notify.error("Введите название команды");
            return;
        }

        $.get("/User/Command/CreateTeam/", { Name: name })
            .done(function () {
                // TODO Убрать notify
                notify.success("Ok");
                location.reload();
            })
            .fail(function () {
                notify.error("Не удалось создать команду");
            });
    }
}

var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel, $('#page-content').get(0));
}

function ViewModel() {
    this.inputNewCommandName = ko.observable();

    this.createCommand = function () {
        model.createCommand(this.inputNewCommandName());
    }
}
