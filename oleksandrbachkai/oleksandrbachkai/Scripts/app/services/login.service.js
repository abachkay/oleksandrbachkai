(function (angular) {
    angular
        .module("app")
        .factory("loginService", loginService);

    loginService.$inject = ["$http", "$cookies"];

    function loginService($http, $cookies) {
        return {
            login: login,
            register: register,
            getExternalLogins: getExternalLogins,
            registerExternal: registerExternal,
            logout: logout,
            getUserInfo: getUserInfo,
            confirmEmail: confirmEmail
    }       

        function login(email, password) {
            return $http({
                method: "POST",
                url: "/Token",
                headers: { 'Content-Type': "x-www-form-urlencoded" },
                data: "grant_type=password&username=" + email + "&password=" + password
            });
        }

        function register(email, password, passwordConfirm) {
            return $http({
                method: "POST",
                url: "/api/Account/Register",
                data: {
                    Email: email,
                    Password: password,
                    ConfirmPassword: passwordConfirm
                }
            });
        }

        function getExternalLogins() {
            return $http({
                method: "GET",
                url: "/api/account/externallogins?returnUrl=%2F&generateState=true"
            });
        }

        function registerExternal(token, email) {
            return $http({
                method: "POST",
                url: "api/Account/RegisterExternal",
                data: { "Email": email, "Name": email },
                headers: { "authorization": "Bearer " + token }
            });
        } 

        function logout() {
            return $http({
                method: "POST",
                url: "/api/Account/Logout",
                headers: { "Authorization": "Bearer " + $cookies.get("access_token") }
            });
        }

        function getUserInfo() {
            return $http({
                method: "GET",
                url: "/api/Account/UserInfo",
                headers: { "Authorization": "Bearer " + $cookies.get("access_token") }
            });
        }

        function confirmEmail(userId, code) {
            return $http({
                method: "GET",
                url: "/api/Account/ConfirmEmail?userId=" + userId + "&code=" + code  
            });
        }
    }
})(angular);