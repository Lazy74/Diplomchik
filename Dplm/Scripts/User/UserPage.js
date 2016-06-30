
var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel, $('#page-content').get(0));
}

function ViewModel() {

    this.updateUser = function () {
        location.pathname = "User/update/";    // строка пути (относительно хоста)
    }
}