(function (angular) {
    angular
        .module("app")
        .controller("loginController", loginController);
    loginController.$inject = ["$scope", "$cookies", "loginService"];
    function loginController($scope, $cookies, loginService) {
        var vm = this;

        vm.loginEmail = "";
        vm.loginPassword = "";
        vm.login = login;

        vm.registerEmail = "";
        vm.registerPassword = "";
        vm.registerPasswordConfirm = "";
        vm.register = register;

        function login() {           
            loginService.login(vm.loginEmail, vm.loginPassword).then(function (response) {
                alert("logged in");
                //$cookies.put("access_token", response.data.access_token);
                //$scope.$emit("loginEvent");
            }, function (response) {
                vm.errors = "Invalid email or password.";
            }).finally(function() {
                vm.loginEmail = "";
                vm.loginPassword = "";
            });
        }

        function register() {
            loginService.register(vm.registerEmail, vm.registerPassword, vm.registerPasswordConfirm).then(function (response) {
                alert("register");
            }, function (response) {
                vm.errors = [];
                for (var key in response.data['ModelState']) {
                    var arr = response.data['ModelState'][key];
                    for (var i = 0; i < arr.length; i++) {
                        vm.errors.push(arr[i]);
                    }
                }
            }).finally(function () {
                vm.registerEmail = "";
                vm.registerPassword = "";
                vm.registerPasswordConfirm = "";
            });
        }
    }
})(angular);