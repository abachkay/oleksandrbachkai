(function (angular) {
    angular
        .module("app")
        .controller("filesController", linksController);
    linksController.$inject = ['$scope', '$cookies'];
    function linksController($scope,  $cookies) {
        var vm = this;
        vm.title = 'files';
    }
})(angular);