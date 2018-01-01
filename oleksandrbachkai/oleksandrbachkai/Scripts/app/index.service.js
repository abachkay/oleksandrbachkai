(function (angular) {
    angular
        .module("app")
        .factory("indexService", indexService);

    indexService.$inject = ["$http"];

    function indexService($http) {
        return {
            getPages: getPages         
        }         

        function getPages() {
            return $http({
                method: "GET",
                url: "/api/content"
            });
        }
    }
})(angular);