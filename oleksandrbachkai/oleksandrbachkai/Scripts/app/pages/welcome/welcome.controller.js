(function (angular) {
    angular
        .module("app")
        .controller("welcomeController", linksController);
    linksController.$inject = ['$scope', '$cookies'];
    function linksController($scope, $cookies) {
        var vm = this;
        vm.title = 'welcome';
    }
})(angular);