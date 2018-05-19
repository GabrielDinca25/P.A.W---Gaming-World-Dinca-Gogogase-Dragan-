function UserAPI() {
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

    this.getSignedInUser = function () {
        var getSinedInUserReq = '/api/login/GetLoggedInUser'
        return doAsyncGet(getSinedInUserReq);
    }

    this.getAllUsers = function () {
        var getAllUsers = '/api/login/GetAllUsers'
        return doAsyncGet(getAllUsers);
    }

    this.getUserInfo = function () {
        var getUserInfo = '/api/login/GetUserInfo'
        return doAsyncGet(getUserInfo);
    }

    UserAPI.instance = this;
}