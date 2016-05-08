var model = {
    Answer: function (answer) {
        console.log("toAnswer");
        //debugger;
        $.get("/GamePlay/AnswerСheck/", {
            answer: answer
        })
            .done(function () {
                window.location.reload();
                alert("Ответ \"" + answer + "\" верный");
                //location.pathname = "";    // строка пути (относительно хоста)
            })
            .fail(function () {
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