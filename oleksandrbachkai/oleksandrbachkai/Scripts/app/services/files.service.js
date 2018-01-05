(function (angular) {
    angular
        .module("app")
        .factory("filesService", filesService);
    filesService.$inject = ["$http", "$cookies"];
    function filesService($http, $cookies) {
        return {            
            getFolders: getFolders,
            createFolder: createFolder,
            deleteFolder: deleteFolder,
            getFiles: getFiles,
            uploadFile: uploadFile,
            deleteFile: deleteFile,            
        }
      
        function getFolders() {
            return $http({
                method: "GET",
                url: "/api/files/folders",
                headers: { "Authorization": "Bearer " + $cookies.get("access_token") }
            });
        }

        function createFolder(folderName) {
            return $http({
                method: "POST",
                url: "/api/files/folders",
                data: "\'" + folderName + "\'",
                headers: { "Authorization": "Bearer " + $cookies.get("access_token") }
            });
        }

        function deleteFolder(folderId) {
            return $http({
                method: "DELETE",
                url: "/api/files/folders/" + folderId,
                headers: { "Authorization": "Bearer " + $cookies.get("access_token") }
            });
        }

        function getFiles(folderId) {
            return $http({
                method: "GET",
                url: "/api/files/" + folderId,
                headers: { "Authorization": "Bearer " + $cookies.get("access_token") }
            });
        }
      
        function deleteFile(folderId, fileId) {
            return $http({
                method: "DELETE",
                url: "/api/files/" + folderId + "/" + fileId,
                headers: { "Authorization": "Bearer " + $cookies.get("access_token") }
            });
        }

        function uploadFile(folderId, data) {
            return $http({
                method: "POST",
                url: '/api/files/' + folderId,
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined, "Authorization": "Bearer " + $cookies.get("access_token") },
                data: data
            });
        }
    }
})(angular);