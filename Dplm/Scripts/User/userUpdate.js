var model = {
    //createUser: function (pass, repetPass, userLogin, phoneNumber, email) {
    //    if (pass != repetPass) {
    //        notify.error("Пароли не совпадают!");
    //    }
    //    return $.post("/api/Registration/RegPeople", {
    //        UserLogin: userLogin,
    //        UserPass: pass,
    //        PhoneNumber: phoneNumber,
    //        Email: email
    //    });
    //}
    getContent: function () {
        console.log("getContent");
        return $.get('/User/update/GetUserContent');
    },

    updateUser: function (firstName, lastName, phoneNumber, userLogin, email, linkVK, newUserPass) {
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
    loadContent();
}

function ViewModel() {
    that = this;

    this.firstName = ko.observable();
    this.lastName = ko.observable();
    this.phoneNumber = ko.observable();
    this.userLogin = ko.observable();
    this.email = ko.observable();
    this.linkVK = ko.observable();
    this.birthday = ko.observable();
    this.newUserPass = ko.observable();
    this.newConfirmUserPass = ko.observable();

    this.updateUser = function () {
        var newUserPass = this.newUserPass() == this.newConfirmUserPass()
            ? this.newUserPass()
            : null;

        model.updateUser(this.firstName(), this.lastName(), this.phoneNumber(), this.userLogin(), this.email(), this.linkVK(), newUserPass);
        //model.updateUser("firstName", "lastName", "phoneNumber", "userLogin", "email", "linkVK", "newPass", "newPass");
    }
}

function loadContent() {
    model.getContent()
        .done(function (content) {
            that.userLogin(content.UserLogin);
            that.phoneNumber(content.PhoneNumber);
            that.email(content.Email);
            that.lastName(content.FamiluName);
            that.firstName(content.Name);
            that.linkVK(content.LinkVK);

            var birthday = new Date(parseInt(content.Birthday.match(/\d+/)[0]));

            birthday = moment(birthday).format('YYYY-MM-DD');

            that.birthday(birthday);
        })
        .fail(function () {
            notify.error("не удалось получить данные об игре!");
        });
}
