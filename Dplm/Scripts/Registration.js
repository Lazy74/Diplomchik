
var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel);
}

function ViewModel() {
    console.log("ViewModel()");

    this.userLogin = ko.observable();
    this.userPass = ko.observable();
    this.repetUserPass = ko.observable();
    this.phoneNumber = ko.observable();
    this.email = ko.observable();


    this.createUser = function () {
        console.log("Ok");
        alert("пользователь создан!");
    }

}
