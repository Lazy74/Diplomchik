var model = {
    authorizeUser: function (login, pass) {
        console.log("AuthorizeUser");
        //$.get("/api/User/AuthorizeUser/", { Login: login, Pass: pass })
        $.get("/AuthorizeUser/", {
            Login: login,
            Pass: pass
        })
            .done(function () {
                notify.success("Авторизация прошла успешно");
                location.pathname = "";    // строка пути (относительно хоста)
            })
            .fail(function () {
                notify.error("Пользователь не найден!");
            });
        //$.ajax({
        //    url: "/AuthorizeUser/",
        //    type: "POST",
        //    //contentType: "json",
        //    data: {
        //        Login: login,
        //        Pass: pass
        //    },
        //    success: function(rec) {
        //        debugger;
        //    },
        //    error: function(rec) {
        //        debugger;
        //    }
        //});
    },

    toUserTeam: function () {
        $.get("/User/CommandСheck/")
            .done(function () {
                location.href = "/User/Command/";
            })
            .fail(function () {
                notify.error("Для начала авторизуйтесь!");
            });
    }
}

var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel, $('#page-content').get(0));
}

function ViewModel() {
    this.userLogin = ko.observable();
    this.userPass = ko.observable();

    this.Authr = function() {
        // TODO надоело авторизоваться. к показу убрать эту кнопку
        model.authorizeUser("lazy_74", "qweqwe");
    }

    this.authorizeUser = function () {
        model.authorizeUser(this.userLogin(), this.userPass());
    }

    this.createUser = function () {
        location.href = "/Registration/";
    }

    this.toUserPage = function () {
        location.href = "/User/";
    }

    this.toUserTeam = function () {
        model.toUserTeam();
    }

    this.toAdminGame = function () {
        location.href = "/Administration/";
    }
}

//var model = {
//    createUser: function() {
//        $.
//    }
//}

//console.log("location");