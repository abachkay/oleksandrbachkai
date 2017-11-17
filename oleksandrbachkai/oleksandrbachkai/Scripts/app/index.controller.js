(function (angular) {
    angular
        .module("app")
        .controller("indexController", indexController);
    indexController.$inject = ['$scope', '$cookies'];
    function indexController($scope, $cookies) {
        var vm = this;
        vm.title = 'index';              
    }
})(angular);