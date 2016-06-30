var model = {
    createUser: function (pass, repetPass, userLogin, phoneNumber, email) {
        if (pass != repetPass) {
            notify.error("Пароли не совпадают!");
        }
        return $.post("/Registration/NewPeople", {
            UserLogin: userLogin,
            UserPass: pass,
            PhoneNumber: phoneNumber,
            Email: email
        });
    }
}

var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel, $('#page-content').get(0));
}

function ViewModel() {
    console.log("ViewModel()");

    this.userLogin = ko.observable();
    this.userPass = ko.observable();
    this.confirmUserPass = ko.observable();
    this.phoneNumber = ko.observable();
    this.email = ko.observable();

    this.createUser = function () {
        model.createUser(this.userPass(), this.confirmUserPass(), this.userLogin(), this.phoneNumber(), this.email())
            .success(function () {
                notify.success("пользователь создан!");
                location.pathname = "/User/";    // строка пути (относительно хоста)
            })
            .error(function () {
                notify.error("Все пропало :(");
            });

    }

}
