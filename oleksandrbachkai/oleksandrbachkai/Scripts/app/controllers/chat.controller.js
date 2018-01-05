(function (angular) {
    angular
        .module("app")
        .controller("chatController", linksController);
    linksController.$inject = ["$scope", "$cookies"];
    function linksController($scope, $cookies) {
        var vm = this;
        vm.title = "chat";
    }
})(angular);