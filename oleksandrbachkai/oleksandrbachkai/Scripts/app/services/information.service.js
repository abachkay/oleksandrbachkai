(function (angular) {
    angular
        .module("app")
        .factory("informationService", informationService);

    informationService.$inject = ["$http", "$cookies"];

    function informationService($http, $cookies) {
        return {
            getPage: getPage,
            updatePage: updatePage,
            getPages: getPages,
            createPage: createPage,
            deletePage: deletePage
        }         

        function getPages() {
            return $http({
                method: "GET",
                url: "/api/content/names",
                headers: { "Authorization": "Bearer " + $cookies.get("access_token") }
            });
        }

        function getPage(id) {
            return $http({
                method: "GET",
                url: "/api/content/" + id,
                headers: { "Authorization": "Bearer " + $cookies.get("access_token") }
            });
        }

        function createPage(name) {
            return $http({
                method: "POST",
                url: "/api/content",
                data: { Name: name },
                headers: { "Authorization": "Bearer " + $cookies.get("access_token") }
            });
        }

        function deletePage(id) {
            return $http({
                method: "DELETE",
                url: "/api/content/" + id,
                headers: { "Authorization": "Bearer " + $cookies.get("access_token") }
            });
        }    

        function updatePage(id, content) {
            return $http({
                method: "PUT",
                url: "/api/content/" + id +"/content",
                data: "\'" + content + "\'",
                headers: { "Authorization": "Bearer " + $cookies.get("access_token") }
            });
        }
    }
})(angular);