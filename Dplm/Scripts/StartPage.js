var model = {
    authorizeUser: function (login, pass) {
        console.log("AuthorizeUser");
        $.get("/api/User/AuthorizeUser/", {Login: login, Pass: pass});
    }
}

var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel);
}

function ViewModel() {
    
    this.userLogin = ko.observable();
    this.userPass = ko.observable();

    this.authorizeUser = function () {
        model.authorizeUser(this.userLogin(), this.userPass());
    }

    this.createUser = function () {
        location.href = "/Registration/";
    }
}

//var model = {
//    createUser: function() {
//        $.
//    }
//}

//console.log("location");