(function (angular) {
    angular
        .module("app")
        .factory("informationService", informationService);

    informationService.$inject = ["$http"];

    function informationService($http) {
        return {
            getPage: getPage,
            updatePage: updatePage
        }         

        function getPage(id) {
            return $http({
                method: "GET",
                url: "/api/content/" + id
            });
        }

        function updatePage(id, content) {
            return $http({
                method: "PUT",
                url: "/api/content/" + id +"/content",
                data: "\'" + content + "\'"
            });
        }
    }
})(angular);