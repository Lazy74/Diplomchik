﻿var model = {
    authorizeUser: function (login, pass) {
        console.log("AuthorizeUser");
        //$.get("/api/User/AuthorizeUser/", { Login: login, Pass: pass })
        $.get("/AuthorizeUser/", {
                Login: login,
                Pass: pass
            })
            .done(function () {
                alert("Авторизация прошла успешно");
                location.pathname = "";    // строка пути (относительно хоста)
            })
            .fail(function () {
                alert("Пользователь не найден!");
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

    toUserTeam: function() {
        $.get("/User/CommandСheck/")
            .done(function() {
                location.href = "/User/Command/";
            })
            .fail(function() {
                alert("Для начала авторизуйтесь!");
            });
    }
}

var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel);

    // Не нужно!
    //var hash = getCookie("hash");
    //if (!hash) {
    //    //alert("hash нет");
    //} else {
    //    //alert("hash: " + hash);
    //}
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

    this.toUserTeam = function() {
        model.toUserTeam();
    }
}

//var model = {
//    createUser: function() {
//        $.
//    }
//}

//console.log("location");