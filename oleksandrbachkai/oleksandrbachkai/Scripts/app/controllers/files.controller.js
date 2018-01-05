(function (angular) {
    angular
        .module("app")
        .controller("filesController", filesController);
    filesController.$inject = ["$scope", "$cookies", "filesService"];
    function filesController($scope, $cookies, filesService) {
        var vm = this;
        vm.title = "files";                
        vm.getFolders = getFolders;
        vm.deleteFolder = deleteFolder;
        vm.createFolder = createFolder;
        vm.newFolderName = "New folder";
        vm.getFiles = getFiles;
        vm.deleteFile = deleteFile;
        vm.selectFolder = selectFolder;
        vm.uploadFile = uploadFile;
        vm.filesLinks = [];

        getFolders();

        function getFolders() {
            filesService.getFolders().then(function(response) {
                vm.folders = response.data;
                if (vm.folders.length !== 0) {
                    vm.folderId = vm.folders[0].FolderId;
                    getFiles(vm.folderId);
                }
            }, function (response) {
                vm.folders = [];
            });
        }

        function selectFolder(folderId) {
            vm.folderId = folderId;
            getFiles(folderId);
        }

        function createFolder() {
            filesService.createFolder(vm.newFolderName).then(function (response) {
                getFolders();
            });
        }

        function deleteFolder(folderId) {
            filesService.deleteFolder(folderId).then(function (response) {
                getFolders();
            });
        }

        function getFiles(folderId) {
            filesService.getFiles(folderId).then(function (response) {
                vm.filesLinks = response.data;
            }, function (response) {
                vm.filesLinks = [];
            }
            );
        }

        function deleteFile(fileId) {
            filesService.deleteFile(vm.folderId,fileId).then(function (response) {
                getFiles(vm.folderId);
            });
        }

        function uploadFile() {
            var fd = new FormData();
            console.log(vm.files);
            angular.forEach(vm.files,
                function (file) {
                    fd.append("file", file);
                });
            filesService.uploadFile(vm.folderId, fd).then(function() {
                getFiles(vm.folderId);    
            });            
        }
    }
})(angular);