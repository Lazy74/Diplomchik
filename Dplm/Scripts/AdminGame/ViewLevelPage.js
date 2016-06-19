var model = {
    getContentLvl: function () {
        console.log("getContentLVL");
        return $.get('/Administration/EditGameInformation/GetLevelPage', { gameId: window.gameId, lvl: window.lvl });
    },
    getContentAnswer: function () {
        console.log("getContentAnswers");
        return $.get('/Administration/EditGameInformation/GetAnswersOnLvl', { questId: window.lvlId });
    }
}

var viewModel1 = new ViewModel1();

function loadPage() {
    ko.applyBindings(viewModel1);  //возможность работать с моделью представления
}

var that;
//window.lvlId = 0;

function ViewModel1() {
    this.authorComment = ko.observable();
    this.timeout = ko.observable();
    this.textQuest = ko.observable();
    this.questAnswers = ko.observableArray();

    that = this;

    this.save = function () {
        //model.saveContent();
    }

    this.remove = function (obj) {
        that.questAnswers.remove(obj);
    }

    this.add = function () {
        this.questAnswers.push({ Answer: '' });
    }
}

function loadContent() {
    model.getContentLvl()
    .done(function (content) {
        that.authorComment(content.AuthorComment);
        that.timeout(content.TimeOut);
        that.textQuest(content.TextQuest);
        window.lvlId = content.Id;

        model.getContentAnswer()
        .done(function (data) {
                viewModel1.questAnswers(data);
            })
        .fail(function (data) {
        });
    })
    .fail(function (content) {
        alert("не удалось получить данные об игре!");
    });
}

