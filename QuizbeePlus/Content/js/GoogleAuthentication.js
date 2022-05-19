/// <reference path="jquery-3.4.1.js" />
function getAccessToken() {
    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var accessToken = location.hash.split('access_token=')[1].split('&')[0];
            if (accessToken) {
                console.log(accessToken)
                isUserRegistered(accessToken);
            }
        }
    }
}
function isUserRegistered(accessToken) {
    $.ajax({
        url: '/api/App/UserInfo',
        method: 'GET',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (response) {
            console.log(response)
            if (response.HasRegistered) {
                localStorage.setItem('accessToken', accessToken);
                localStorage.setItem('userName', response.Email);
                window.location.href = "Data.html";
            }
            else {
                signupExternalUser(accessToken);
            }
        }
    });
}

function signupExternalUser(accessToken) {
    $.ajax({
        url: '/api/App/RegisterExternal',
        method: 'POST',
        headers: {
            'content-type': 'application/json',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function () {
            window.location.href = "/api/App/ExternalLogin?provider=Google&response_type=token&client_id=self&redirect_uri=https%3A%2F%2Flocalhost%3A44394%2FLogin.html&state=AEHRMUNdZHSP0f5SGCeIbCmbhyFE1Uz6p6RTvYAGGCI1";
        }
    });

}