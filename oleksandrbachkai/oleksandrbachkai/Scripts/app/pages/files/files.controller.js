(function (angular) {
    angular
        .module("app")
        .controller("filesController", linksController);
    linksController.$inject = ['$scope', '$cookies', 'filesFactory'];
    function linksController($scope, $cookies, filesFactory) {
        var vm = this;
        vm.title = 'files';
        vm.getValues = getValues;

        function getValues()
        {
            filesFactory.getValues().then(function (response) {
                console.log(response);
            }, function (response) {
                console.log(response);
            });
        }
    }
})(angular);