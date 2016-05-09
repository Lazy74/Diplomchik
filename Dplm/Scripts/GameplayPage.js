var model = {
    Answer: function (answer) {
        console.log("toAnswer");
        //debugger;
        $.get("/GamePlay/AnswerСheck/", {
            answer: answer
        })
            .done(function () {
                ReloadPage();
                alert("Ответ \"" + answer + "\" верный");
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
    viewModel.myMessage("Test");
    ko.applyBindings(viewModel);
}

function ViewModel() {
    this.playerAnswer = ko.observable();
    this.myMessage = ko.observable();


    this.toAnswer = function () {
        model.Answer(this.playerAnswer());
    }
}


//var timerId = setTimeout(function () {
//    //window.location.reload();
//    alert("Перезагрузка!");
//}, 5000);


var sec = 300;
// Это работает
var timerId = setTimeout(function tick() {
    timerId = setTimeout(tick, 1000);
    sec = sec - 1;
    viewModel.myMessage(sec);
    var now = new Date();
    console.log(now);
    //window.location.reload();
}, 0);