var model = {
    authorizeUser: function (login, pass) {
        console.log("AuthorizeUser");
        //$.get("/api/User/AuthorizeUser/", { Login: login, Pass: pass })
        $.get("/AuthorizeUser/", { Login: login, Pass: pass })
            .success(function(dataResult) {
                if (dataResult) {
                    setCookie("hash", dataResult);
                    alert("Авторизация прошла успешно!")
                } else {
                    alert("Пользователь не найден!");
                }
            });
        //.error;       если будет ошибка
    }
}

var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel);
    var hash = getCookie("hash");
    if (!hash) {
        alert("hash нет");
    } else {
        alert("hash: " + hash);
    }
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

    this.toUserPage = function() {
        location.href = "/User/";
    }
}

//var model = {
//    createUser: function() {
//        $.
//    }
//}

//console.log("location");