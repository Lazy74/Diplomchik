var model = {
    Answer: function (answer) {
        console.log("toAnswer");
        //debugger;
        $.get("/GamePlay/AnswerСheck/id=" + window.currentGameId, {
            answer: answer
        })
            .done(function () {
                ReloadPage();
                alert("Ответ \"" + answer + "\" верный");
                location.reload();
                //location.pathname = "";    // строка пути (относительно хоста)
            })
            .fail(function () {
                alert("Ответ \"" + answer + "\" не верный");
            });
    }
}

function ReloadPage() {
    window.location.reload();
}

var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel);
}

function ViewModel() {
    this.playerAnswer = ko.observable();

    this.timeNextLvl = ko.observable();
    //this.timeHour = ko.observable();
    //this.timeMinute = ko.observable();
    //this.timeSecond = ko.observable();


    this.toAnswer = function () {
        model.Answer(this.playerAnswer());
    }
}


//var timerId = setTimeout(function () {
//    //window.location.reload();
//    alert("Перезагрузка!");
//}, 5000);


//var sec = 7205;
// Это работает

// Таймер выводящий время до автоперехода и делающий автопереход
var timerId = setTimeout(function tick() {
    if (sec - 1 > 0) {
        timerId = setTimeout(tick, 1000);
        sec = sec - 1;
        viewModel.timeNextLvl(ConvertSecondInTime(sec));
    } else {
        ReloadPage();
    }
}, 0);


// TODO ВРЕМЯ ИДЕТ БЫСТРЕЕ ЧЕМ НАДО!!!
// Вывод оставшегося времени до автоперехода
function ConvertSecondInTime(sec) {
    var result = "";

    n = Math.ceil(sec / 3600) - 1;    // Получаем часы

    if (n !== 0) {
        sec = sec % 3600;
        result = result + n + "ч ";
    }
    if (sec !== 0) {
        n = Math.ceil(sec / 60) - 1;    // Получаем минуты

        if (n !== 0) {
            sec = sec % 60;
            result = result + n + "м ";
        }

        if (sec !== 0) {
            result = result + sec + "c";
        }
    }


    return result;
}