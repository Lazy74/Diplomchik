var model = {
    getContent: function () {
        console.log("getContent");
        return $.get('/GetFullInfoGame/id=' + window.currentGameId);
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
}

function loadContent() {
    model.getContent()
        .done(function(content) {
            that.nameGame(content.NameGame);
            that.sequence(content.Sequence);
            that.distance(content.Distance);
            that.startGame(content.StartGame);
            that.endGame(content.EndGame);
            that.infoContent(content.Info);
        })
        .fail(function(content) {
            alert("не удалось получить данные об игре!");
        });
}

