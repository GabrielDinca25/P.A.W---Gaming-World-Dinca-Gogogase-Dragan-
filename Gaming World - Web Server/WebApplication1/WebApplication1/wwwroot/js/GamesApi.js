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

    this.getGameToAdd = function (id) {
        var gameToAddReq = "/api/cart/1";
        return doAsyncGet(gameToAddReq);
    };

    GamesApi.instance = this;
}