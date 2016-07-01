var model = {
    getContentLvl: function () {
        console.log("getContentLVL");
        return $.get('/Administration/EditGameInformation/GetLevelPage', { gameId: window.gameId, lvl: window.lvl });
    },
    getContentAnswer: function () {
        console.log("getContentAnswers");
        return $.get('/Administration/EditGameInformation/GetAnswersOnLvl', { questId: window.lvlId });
    },
    saveContent: function (answer, authorComment, timeout, textQuest, nameLevel) {
        //new Promise(function () {
        //});
        var s1 = $.post('/Administration/EditGameInformation/UpdateLevel', {
            nameLevel: nameLevel,
            authorComment: authorComment,
            timeout: timeout,
            textQuest: textQuest,
            gameId: window.gameId,
            numberLevel: window.lvl
        })
            .done(function () {
                //var lvlText = "Информация об уровне обновлена!";
            })
            .fail(function () {
                notify.error("Ошибка во время сохранения информации об уровне");
                //var lvlText = "Информация об уровне не обновлена!";
            });
        var s2 = $.post('/Administration/EditGameInformation/UpdateAnswersOnLvl', { Answer: answer, gameId: window.gameId, lvl: window.lvl })
            .done(function () {
            })
            .fail(function () {
                notify.error("Ошибка во время сохранения ответов");
            });
        $.when(s1, s2).then(function () {
            notify.success("Информация обновлена!");
            loadContent();
        });
    },
    deleteAnswer: function (data) {
        $.post('/Administration/EditGameInformation/DeleteAnswer', { Answer: data });
    }
}

var viewModel1 = new ViewModel1();

function loadPage() {
    ko.applyBindings(viewModel1, $('#page-content').get(0));  //возможность работать с моделью представления
}

var that;

function ViewModel1() {
    this.authorComment = ko.observable();
    this.timeout = ko.observable();
    this.textQuest = ko.observable();
    this.nameLevel = ko.observable();
    this.questAnswers = ko.observableArray();

    that = this;

    this.save = function () {
        //model.updateContentLvl();
        //var Answ = this.questAnswers();
        //model.updateContentAnswer(Answ);

        model.saveContent(that.questAnswers(), that.authorComment(), that.timeout(), that.textQuest(), that.nameLevel());
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
        that.nameLevel(content.NameLevel);
        window.lvlId = content.Id;

        model.getContentAnswer(that.questAnswers())
        .done(function (data) {
            viewModel1.questAnswers(data);
        })
        .fail(function (data) {
        });
    })
    .fail(function (content) {
        notify.error("не удалось получить данные об игре!");
    });
}



//Возможно больше не пригодится
//updateContentAnswer: function (answer) {

//    for (var i = 0; i < answer.length; i++) {
//        console.log(answer[i]);
//    }

//    $.post('/Administration/EditGameInformation/UpdateAnswersOnLvl', { Answer: answer, gameId: window.gameId, lvl: window.lvl })
//    .done(function () {
//        notify.success("Ответы обновлены");
//        loadContent();
//    })
//    .fail(function () {
//        notify.error("Ошибка во время сохранения ответов");
//    });
//},
//updateContentLvl: function (authorComment, timeout, textQuest) {
//    $.post('/Administration/EditGameInformation/UpdateLevel', {
//        authorComment: authorComment,
//        timeout: timeout,
//        textQuest: textQuest
//    });
//},