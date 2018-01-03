(function (angular) {
    angular
        .module("app")
        .factory("filesService", filesService);
    filesService.$inject = ["$http"];
    function filesService($http) {
        return {
            postFile: postFile            
        }

        function postFile(data) {
            $http.post('/api/content/file',
                data,
                {
                    transformRequest: angular.identity,
                    headers: { 'Content-Type': undefined }
                });
        }
    }
})(angular);