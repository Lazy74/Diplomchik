
var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel);
}

function ViewModel() {

    this.updateUser = function () {
        location.pathname = "User/update/";    // строка пути (относительно хоста)
    }
}