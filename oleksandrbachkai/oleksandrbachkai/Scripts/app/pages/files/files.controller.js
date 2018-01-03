(function (angular) {
    angular
        .module("app")
        .controller("filesController", linksController);
    linksController.$inject = ['$scope', '$cookies', 'filesService'];
    function linksController($scope, $cookies, filesService) {
        var vm = this;
        vm.title = 'files';
        vm.files = null;
        vm.upload = upload;

        function upload() {
            var fd = new FormData();
            console.log(vm.files);
            angular.forEach(vm.files,
                function(file) {
                    fd.append('file', file);
                });
            filesService.postFile(fd);
        }
    }
})(angular);