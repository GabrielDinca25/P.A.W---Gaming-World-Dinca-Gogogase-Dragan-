function ReviewsApi() {
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

    this.getAllReviews = function () {
        var allReviewsReq = "/api/reviews/GetReviews";
        console.log(allReviewsReq);
        return doAsyncGet(allReviewsReq);
    };
}