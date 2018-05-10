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

function handleClick(cb) {
    var gamesApi = new GamesApi();
    if (cb.checked) {
        var games = gamesApi.getAllGames(5);
        games.done(
            function (response) {
                $("#productGrid").html("");
                for (var i = 0; i < response.length; i++) {
                    if (response[i].genre == cb.name) {
                        $("#productGrid").append('<div style="display: inline-block; padding-right:10px;" class="image-main-section"><form action="http://localhost:50209/api/cart" method="get"><div class="img-part"><div class="img-section"><img src="/images/' + response[i].image + '"></div><div class="image-title"><h3><a class="styleless-link" href="">' + response[i].name + '</a></h3></div><div><input type="hidden" readonly name="gameName" value="' + response[i].name + '"></div> <div class="image-description"><p>$' + response[i].keyPrice + '</p></div><div><input type="submit" class="btn btn-warning add-cart-btn" value="ADD TO CART"></div></form></div>');
                    }
                }
            });
    }
    else
    {
        var games = gamesApi.getAllGames(5);
        games.done(
            function (response) {
                $("#productGrid").html("");
                for (var i = 0; i < response.length; i++) {
                    $("#productGrid").append('<div style="display: inline-block; padding-right:10px;" class="image-main-section"><form action="http://localhost:50209/api/cart" method="get"><div class="img-part"><div class="img-section"><img src="/images/' + response[i].image + '"></div><div class="image-title"><h3><a class="styleless-link" href="">' + response[i].name + '</a></h3></div><div><input type="hidden" readonly name="gameName" value="' + response[i].name + '"></div> <div class="image-description"><p>$' + response[i].keyPrice + '</p></div><div><input type="submit" class="btn btn-warning add-cart-btn" value="ADD TO CART"></div></form></div>');
                }
            });
    }
}
