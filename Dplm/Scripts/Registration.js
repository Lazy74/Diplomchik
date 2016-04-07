var model = {
    createUser: function (pass, repetPass, userLogin, phoneNumber, email) {
        if (pass != repetPass) {
            alert("Пароли не совпадают!");
        }
        return $.post("/api/Registration/RegPeople", {
            UserLogin: userLogin,
            UserPass: pass,
            PhoneNumber: phoneNumber,
            Email: email
        });
    }
}

var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel);
}

function ViewModel() {
    console.log("ViewModel()");

    this.userLogin = ko.observable();
    this.userPass = ko.observable();
    this.confirmUserPass = ko.observable();
    this.phoneNumber = ko.observable();
    this.email = ko.observable();

    this.createUser = function () {
        console.log("Ok");
        model.createUser(this.userPass(), this.confirmUserPass(), this.userLogin(), this.phoneNumber(), this.email())
            .success(function () {
                alert("пользователь создан!");
                //debugger;
            })
            .error(function () {
                alert("Все пропало :(");
            });

    }

}
