(function (angular) {
    angular
        .module("app")
        .controller("informationController", linksController);
    linksController.$inject = ['$scope', '$cookies'];
    function linksController($scope, $cookies) {
        var vm = this;
        vm.title = 'info';
    }
})(angular);