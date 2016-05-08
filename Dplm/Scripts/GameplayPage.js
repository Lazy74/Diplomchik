var model = {
    Answer: function (answer) {
        console.log("toAnswer");
        //debugger;
        $.get("/GamePlay/AnswerСheck/", {
            answer: answer
        })
            .done(function (r) {
                debugger;
                alert("Верный");
                //location.pathname = "";    // строка пути (относительно хоста)
            })
            .fail(function (r) {
                debugger;
                alert("Ответ \"" + answer + "\" не верный");
            });
    }
}

var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel);
}

function ViewModel() {
    this.playerAnswer = ko.observable();

    this.toAnswer = function () {
        model.Answer(this.playerAnswer());
    }
}