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
        var allGamesReq = "/api/games/GetGames/" + id;
        console.log(allGamesReq);
        return doAsyncGet(allGamesReq);
    };


    this.getGameToAdd = function (id) {
        var gameToAddReq = "/api/cart/GetCartProducts";
        return doAsyncGet(gameToAddReq);
    };

    GamesApi.instance = this;
}

function handleClick() {
    var gamesApi = new GamesApi();
    var platforms = $.map($('input:checkbox[name=Platform]:checked'), function (e, i) {
        return e.value;
    });

    var genres = $.map($('input:checkbox[name=Genre]:checked'), function (e, i) {
        return e.value;
    });

    var prices = $.map($('input:checkbox[name=Price]:checked'), function (e, i) {
        return e.value;
    });

    var games = gamesApi.getAllGames(5);
    games.done(
        function (response) {
            $("#productGrid").html("");
            var noResults = true;
            for (var i = 0; i < response.length; i++) {
                var eligible = true;
                platforms.forEach(function (platform) {
                    if (response[i].platform != platform) {
                        eligible = false;
                    }
                });
                genres.forEach(function (genre) {
                    if (response[i].genre != genre) {
                        eligible = false;
                    }
                });
                var currentPrice = parseFloat(response[i].keyPrice)
                prices.forEach(function (pricerange) {
                    if (pricerange == '5') {
                        if (currentPrice > 5) {
                            eligible = false;
                        }
                    }
                    else if (pricerange == '50') {
                        if (currentPrice < 50) {
                            eligible = false;
                        }
                    }
                    else {
                        var range = pricerange.split('-');
                        var low = parseFloat(range[0]);
                        var high = parseFloat(range[1]);
                        if (currentPrice < low || currentPrice > high) {
                            eligible = false;
                        }
                    }
                    
                });
                if (eligible) {
                    noResults = false;
                    $("#productGrid").append('<div style="display: inline-block; padding-right:10px;" class="image-main-section"><form action="http://localhost:50209/api/cart" method="get"><div class="img-part"><div class="img-section"><img src="/images/' + response[i].image + '"></div><div class="image-title"><h3><a class="styleless-link" href="">' + response[i].name + '</a></h3></div><div><input type="hidden" readonly name="gameName" value="' + response[i].name + '"></div> <div class="image-description"><p>$' + response[i].keyPrice + '</p></div><div><input type="submit" class="btn btn-warning add-cart-btn" value="ADD TO CART"></div></form></div>');
                }
            }
            if (noResults == true) {
                $("#productGrid").html('<h1 style="color: white;">No results found</h1>');
            }
        });
}

function SearchResultsAPI() {
    var doAsyncSearchGet = function (searchTerm) {
        var authorityToken = "";
        var fullUrl = 'http://localhost:50209' + "/api/games/GetSearchResults/";
        return $.ajax({
            url: fullUrl,
            data: { "search": searchTerm },
            async: false,
            headers: {
                "Authority": authorityToken
            },
            dataType: "json"
        });
    };

    this.getSearchResults = function(searchTerm)
    {
        var products = doAsyncSearchGet(searchTerm)
        return products;
    }

    SearchResultsAPI .instance = this;
};

function displaySearchResults(searchTerm) {
    var searchResultsAPI = new SearchResultsAPI();
    var games = searchResultsAPI.getSearchResults(searchTerm);
    games.done(
        function (response) {
            $("#productGrid").html("");
            for (var i = 0; i < response.length; i++) {
                $("#productGrid").append('<div style="display: inline-block; padding-right:10px;" class="image-main-section"><form action="http://localhost:50209/api/cart/Add" method="get"><div class="img-part"><div class="img-section"><img src="/images/' + response[i].image + '"></div><div class="image-title"><h3><a class="styleless-link" href="">' + response[i].name + '</a></h3></div><div><input type="hidden" readonly name="gameName" value="' + response[i].name + '"></div> <div class="image-description"><p>$' + response[i].keyPrice + '</p></div><div><input type="submit" class="btn btn-warning add-cart-btn" value="ADD TO CART"></div></form></div>');
            }
        });
}
