var notify = (function () {
    return {
        success: success,
        error: error
    };

    function success(text) {
        ohSnap(text, { color: 'green'});
    }

    function error(text) {
        ohSnap(text, { color: 'red' });
    }
})();