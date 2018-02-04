(function (angular) {
    angular
        .module("app")
        .controller("loginController", loginController);
    loginController.$inject = ["$scope", "$cookies", "loginService", "$location"];
    function loginController($scope, $cookies, loginService, $location) {
        var vm = this;
        vm.loginEmail = "";
        vm.loginPassword = "";
        vm.login = login;
        vm.loginErrors = null;
        vm.registerEmail = "";
        vm.registerPassword = "";
        vm.registerPasswordConfirm = "";
        vm.register = register;
        vm.registerErrors = null;
        vm.loginGoogle = loginGoogle;

        function login() {           
            loginService.login(vm.loginEmail, vm.loginPassword).then(function (response) {
                loginComplete(response.data);
            }, function (response) {
                vm.loginErrors = "Invalid email or password";
            }).finally(function() {
                vm.loginEmail = "";
                vm.loginPassword = "";
            });
        }

        function loginComplete(data) {
            $cookies.put("access_token", data.access_token);
            $scope.$emit("loggedIn");
            vm.loginErrors = null;
            $location.path("/");
        }

        function register() {
            loginService.register(vm.registerEmail, vm.registerPassword, vm.registerPasswordConfirm).then(function (response) {                
                vm.registerErrors = null;         
                vm.registerEmail = "";
                vm.registerPassword = "";
                vm.registerPasswordConfirm = "";
            }, function (response) {
                vm.registerErrorsArray = [];
                vm.registerErrors = "";
                for (var key in response.data["ModelState"]) {
                    var arr = response.data["ModelState"][key];
                    for (var i = 0; i < arr.length; i++) {
                        vm.registerErrorsArray.push(arr[i]);
                        vm.registerErrors += arr[i];
                    }
                }
            }).finally(function () {
                vm.registerEmail = "";
                vm.registerPassword = "";
                vm.registerPasswordConfirm = "";
            });
        }

        function loginGoogle() {            
            loginService.getExternalLogins().then(function (response) {                
                location.href = response.data[0].Url;
            });
        }
    }
})(angular);