var model = {
    getContentLvl: function () {
        console.log("getContent");
        return $.get('/Administration/EditGameInformation/GetLevelPage', { gameId: window.gameId, lvl: window.lvl });
    }
}

var viewModel1 = new ViewModel1();

function loadPage() {
    ko.applyBindings(viewModel1);  //возможность работать с моделью представления
}

var that;

function ViewModel1() {
    this.authorComment = ko.observable();
    this.timeout = ko.observable();
    this.textQuest = ko.observable();

    that = this;

    this.save = function () {
        //model.saveContent();
    }
}

function loadContent() {
    model.getContentLvl()
    .done(function (content) {
        that.authorComment(content.AuthorComment);
        that.timeout(content.TimeOut);
        that.textQuest(content.TextQuest);
    })
    .fail(function (content) {
        alert("не удалось получить данные об игре!");
    });
}

