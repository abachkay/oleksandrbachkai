(function (angular) {
    angular
        .module("app")
        .factory("filesService", filesService);
    filesService.$inject = ["$http"];
    function filesService($http) {
        return {
            postFile: postFile,
            getFolders: getFolders,
            createFolder: createFolder,
            deleteFolder: deleteFolder,
            getFiles: getFiles,
            uploadFile: uploadFile,
            deleteFile: deleteFile,
            addFile: addFile
    }

        function postFile(data) {
            return $http({
                method: "POST",
                url: '/api/content/file',
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined },
                data: data
            });            
        }

        function getFolders() {
            return $http({
                method: "GET",
                url: "/api/files/folders"
            });
        }

        function createFolder(folderName) {
            return $http({
                method: "POST",
                url: "/api/files/folders",
                data: "\'" + folderName + "\'"
            });
        }

        function deleteFolder(folderId) {
            return $http({
                method: "DELETE",
                url: "/api/files/folders/" + folderId,                
            });
        }

        function getFiles(folderId) {
            return $http({
                method: "GET",
                url: "/api/files/" + folderId
            });
        }

        function uploadFile(folderId, data) {
            return $http({
                method: "POST",
                url: '/api/files/' + folderId,
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined },
                data: data
            });
        }

        function deleteFile(folderId, fileId) {
            return $http({
                method: "DELETE",
                url: "/api/files/" + folderId + "/" + fileId
            });
        }

        function addFile(folderId, data) {
            return $http({
                method: "POST",
                url: '/api/files/' + folderId,
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined },
                data: data
            });
        }
    }
})(angular);