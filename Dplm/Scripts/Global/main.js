$(function () {
    ko.applyBindings({
        menuItems: ko.observableArray([
            {
                text: 'Моя команда',
                action: function() {
                    console.log('Click Моя команда');
                }
            },
            {
                text: 'Игры',
                action: function () {
                    console.log('Click Игры');
                }
            }
        ])
    }, $('#menu-container').get(0));
});