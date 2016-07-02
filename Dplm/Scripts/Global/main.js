(function () {
    var viewModel_menu = {
        menuItems: ko.observableArray([
            {
                text: 'Моя страница',
                action: function () {
                    location.href = "/User/";
                },
                visible: true
            },
            {
                text: 'Моя команда',
                action: function () {
                    $.get("/User/CommandСheck/")
                        .done(function () {
                            location.href = "/User/Command/";
                        })
                        .fail(function () {
                            notify.error("Для начала авторизуйтесь!");
                        });
                },
                visible: true
            },
            {
                text: 'Игры',
                action: function () {
                    location.pathname = "";
                },
                visible: true
            },
            {
                text: 'Вход',
                action: function () {
                    $('#dark-bg').fadeIn(200);
                    $('#login-block').fadeIn(200);
                },
                visible: ko.observable(!!!getCookie('hash'))
            },
            {
                text: 'Регистрация',
                action: function () {
                    location.href = "/Registration/";
                },
                visible: ko.observable(!!!getCookie('hash'))
            },
            {
                text: 'Выход',
                action: function () {
                    $.get('/logout')
                        .done(function () {
                            location.pathname = '';
                        })
                        .fail(function () {
                            // 
                        });
                },
                visible: ko.observable(!!getCookie('hash'))
            }
        ])
    };
    var viewModel_login = {
        userLogin: ko.observable(),
        userPass: ko.observable(),
        authorizeUser: function () {
            $.get("/AuthorizeUser/", {
                Login: this.userLogin(),
                Pass: this.userPass()
            })
            .done(function () {
                notify.success("Авторизация прошла успешно");
                location.pathname = "";    // строка пути (относительно хоста)
            })
            .fail(function () {
                notify.error("Пользователь не найден!");
            });
        }
    };
    $(function () {
        ko.applyBindings(viewModel_menu, $('#menu-container').get(0));
        ko.applyBindings(viewModel_login, $('#login-block').get(0));
        $('#dark-bg').click(function () {
            $(this).fadeOut(200);
            $('#login-block').fadeOut(200);
        });
    });
})();