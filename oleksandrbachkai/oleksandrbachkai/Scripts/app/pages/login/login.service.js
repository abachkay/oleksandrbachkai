(function (angular) {
    angular
        .module("app")
        .factory("loginService", loginService);
    loginService.$inject = ["$http"];
    function loginService($http) {
        return {
            login: login,
            register: register
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
    }
})(angular);