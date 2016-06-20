var model = {
    getContent: function () {
        console.log("getContent");
        return $.get('/Administration/GetFullInfoGame/id=' + window.currentGameId);
    },

    saveContent: function(nameGame, sequence, distance, startGame, endGame, infoContent) {
        debugger;
        $.post('/Administration/UpdateInfoGame/', {
            id: window.currentGameId,
            nameGame: nameGame,
            sequence: sequence,
            distance: distance,
            startGame: startGame,
            endGame: endGame,
            info: infoContent
            //info: window.btoa(unescape(encodeURIComponent(infoContent)))
            })
            .done(function () {
                alert("Данные обновлены");
            }).fail(function() {
                alert("Ошибка вовремя обновления, данные остались без изменения");
        });
    } 
}

var viewModel1 = new ViewModel1();

function loadPage() {
    ko.applyBindings(viewModel1);  //возможность работать с моделью представления
}


var that;

function ViewModel1() {
    this.nameGame = ko.observable();
    this.sequence = ko.observable();
    this.distance = ko.observable();
    this.startGame = ko.observable();
    this.endGame = ko.observable();
    this.infoContent = ko.observable();

    that = this;

    this.save = function () {
        var infoText = this.infoContent();
        model.saveContent(this.nameGame(), this.sequence(), this.distance(), this.startGame(), this.endGame(), infoText);
    }
}

function loadContent() {
    model.getContent()
        .done(function(content) {
            that.nameGame(content.NameGame);
            that.sequence(content.Sequence);
            that.distance(content.Distance);
            //that.startGame(content.StartGame);
            //that.endGame(content.EndGame);
            that.infoContent(content.Info);

            var stime = new Date(parseInt(content.StartGame.match(/\d+/)[0]));
            var entime = new Date(parseInt(content.EndGame.match(/\d+/)[0]));

            stime = moment(stime).format('YYYY-MM-DDTHH:mm');
            entime = moment(entime).format('YYYY-MM-DDTHH:mm');

            that.startGame(stime);
            that.endGame(entime);
        })
        .fail(function(content) {
            alert("не удалось получить данные об игре!");
        });
}

