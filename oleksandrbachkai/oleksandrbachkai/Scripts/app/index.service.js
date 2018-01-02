(function (angular) {
    angular
        .module("app")
        .factory("indexService", indexService);

    indexService.$inject = ["$http"];

    function indexService($http) {
        return {
            getPages: getPages,
            createPage: createPage,
            deletePage: deletePage            
        }         

        function getPages() {
            return $http({
                method: "GET",
                url: "/api/content/names"
            });
        }

        function createPage(name) {
            return $http({
                method: "POST",
                url: "/api/content",
                data: { Name: name }
            });
        }

        function deletePage(id) {
            return $http({
                method: "DELETE",
                url: "/api/content/"+id                
            });
        }      
    }
})(angular);