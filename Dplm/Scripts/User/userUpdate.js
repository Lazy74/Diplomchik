var model = {
    //createUser: function (pass, repetPass, userLogin, phoneNumber, email) {
    //    if (pass != repetPass) {
    //        alert("Пароли не совпадают!");
    //    }
    //    return $.post("/api/Registration/RegPeople", {
    //        UserLogin: userLogin,
    //        UserPass: pass,
    //        PhoneNumber: phoneNumber,
    //        Email: email
    //    });
    //}

    updateUser: function(firstName, lastName, phoneNumber, userLogin, email, linkVK, newUserPass, userPass) {
        $.post("/api/UpdateUser/updatePeople", {
            UserLogin: userLogin,
            UserPass: newUserPass,
            PhoneNumber: phoneNumber,
            Email: email,
            FamiluName: lastName,
            Name: firstName,
            //Birthday, 
            LinkVK: linkVK
        });
    }
}

var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel);
}

function ViewModel() {

    this.firstName = ko.observable();
    this.lastName = ko.observable();
    this.phoneNumber = ko.observable();
    this.userLogin = ko.observable();
    this.email = ko.observable();
    this.linkVK = ko.observable();
    this.newUserPass = ko.observable();
    this.newConfirmUserPass = ko.observable();
    this.userPass = ko.observable();

    this.updateUser = function() {
        // TODO сделать проверку ввели ли новый пароль. а пока пароль менять нельзя!
        var newPass = this.userPass();

        model.updateUser(this.firstName(), this.lastName(), this.phoneNumber(), this.userLogin(), this.email(), this.linkVK(), newPass, this.userPass());
        //model.updateUser("firstName", "lastName", "phoneNumber", "userLogin", "email", "linkVK", "newPass", "newPass");
    }

    //this.createUser = function () {
        //model.createUser(this.userPass(), this.confirmUserPass(), this.userLogin(), this.phoneNumber(), this.email())
        //    .success(function () {
        //        alert("пользователь создан!");
        //        location.pathname = "";    // строка пути (относительно хоста)
        //    })
        //    .error(function () {
        //        alert("Все пропало :(");
        //    });

}

