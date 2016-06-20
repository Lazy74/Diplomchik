var model = {
    getContentLvl: function () {
        console.log("getContentLVL");
        return $.get('/Administration/EditGameInformation/GetLevelPage', { gameId: window.gameId, lvl: window.lvl });
    },

    getContentAnswer: function () {
        console.log("getContentAnswers");
        return $.get('/Administration/EditGameInformation/GetAnswersOnLvl', { questId: window.lvlId });
    },

    updateContentAnswer: function (datas) {

        for (var i = 0; i < datas.length; i++) {
            console.log(datas[i]);
        }

        $.post('/Administration/EditGameInformation/UpdateAnswersOnLvl', { Answer: datas, gameId: window.gameId, lvl: window.lvl })
        .done(function () {
            alert("Ответы обновлены");
            loadContent();
        })
        .fail(function () {
            alert("Ошибка во время сохранения ответов");
        });
    },

    deleteAnswer: function (data) {
        $.post('/Administration/EditGameInformation/DeleteAnswer', { Answer: data });
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
    this.questAnswers = ko.observableArray();

    that = this;

    this.save = function () {
        //model.saveContent();

        var Answ = this.questAnswers();

        model.updateContentAnswer(Answ);
    }

    this.remove = function (obj) {
        model.deleteAnswer(obj);
        that.questAnswers.remove(obj);
    }

    this.add = function () {
        this.questAnswers.push({ TextAnswer: '' });
    }
}

function loadContent() {
    model.getContentLvl()
    .done(function (content) {
        that.authorComment(content.AuthorComment);
        that.timeout(content.TimeOut);
        that.textQuest(content.TextQuest);
        window.lvlId = content.Id;

        model.getContentAnswer(that.questAnswers())
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

