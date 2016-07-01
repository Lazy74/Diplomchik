(function () {
    var viewModel = {
        menuItems: ko.observableArray([
            {
                text: 'Моя команда',
                action: function () {
                    location.href = "/User/";
                },
                visible: true
            },
            {
                text: 'Игры',
                action: function () {
                    console.log('Click Игры');
                },
                visible: true
            },
            {
                text: 'Вход',
                action: function () {

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
    $(function () {
        ko.applyBindings(viewModel, $('#menu-container').get(0));
    });
})();