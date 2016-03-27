var viewModel = new ViewModel();

function loadPage() {
    ko.applyBindings(viewModel);
}

function ViewModel() {
    
    this.userLogin = ko.observable();
    this.userPass = ko.observable();

    this.authorizeUser = function () {
    }

    this.createUser = function () {
        location.href = "/Registration/";
    }
}

//var model = {
//    createUser: function() {
//        $.
//    }
//}

//console.log("location");