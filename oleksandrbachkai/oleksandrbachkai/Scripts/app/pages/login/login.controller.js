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
        vm.loginErrors = null;

        vm.registerEmail = "";
        vm.registerPassword = "";
        vm.registerPasswordConfirm = "";
        vm.register = register;
        vm.registerErrors = null;

        vm.loginGoogle = loginGoogle;

        function login() {           
            loginService.login(vm.loginEmail, vm.loginPassword).then(function (response) {                
                //alert("logged in");
                //$cookies.put("access_token", response.data.access_token);
                //$scope.$emit("loginEvent");
                vm.loginErrors = null;
            }, function (response) {
                vm.loginErrors = "Invalid email or password";
            }).finally(function() {
                vm.loginEmail = "";
                vm.loginPassword = "";
            });
        }

        function register() {
            loginService.register(vm.registerEmail, vm.registerPassword, vm.registerPasswordConfirm).then(function (response) {                
                vm.registerErrors = null;
            }, function (response) {
                vm.registerErrorsArray = [];
                vm.registerErrors = "";
                for (var key in response.data['ModelState']) {
                    var arr = response.data['ModelState'][key];
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
                console.log(response);
                location.href = response.data[0].Url;
            }, function (response) {
               
            }).finally(function () {
               
            });
        }
    }
})(angular);