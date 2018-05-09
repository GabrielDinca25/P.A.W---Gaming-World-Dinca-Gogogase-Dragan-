function GamesApi() {
    var doAsyncGet = function (partialUrl) {
        var authorityToken = "";
        var fullUrl = 'http://localhost:50209' + partialUrl;
        return $.ajax({
            url: fullUrl,
            headers: {
                "Authority": authorityToken
            },
            dataType: "json"
        });
    };

    this.getAllGames = function (id) {
        var allGamesReq = "/api/games/" + id;
        console.log(allGamesReq);
        return doAsyncGet(allGamesReq);
    };

    GamesApi.instance = this;
}