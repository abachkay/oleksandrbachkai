(function (angular) {
    angular
        .module("app")
        .controller("informationController", informationController);
    informationController.$inject = ["$scope", "$rootScope", "$cookies", "$sce", "informationService", "$timeout"];
    function informationController($scope, $rootScope, $cookies, $sce, informationService, $timeout) {
        var vm = this;
        vm.title = "info";
        vm.content = "";        
        vm.updatePage = updatePage;
        vm.edit = false;
        vm.openEditor = openEditor;      

        $scope.$on("pageSelected", function (event, pageId) {
            getPage(pageId);
        });     
                
        function openEditor() {
            vm.edit = true;
        }

        function getPage(pageId) {
            informationService.getPage(pageId).then(function (response) {
                vm.pageId = pageId;
                vm.content = response.data.Content;
                vm.renderHtml();
            });
        }       

        vm.renderHtml = function () {
            return $sce.trustAsHtml(vm.content);
        };

        function updatePage() {
            if (vm.pageId) {                
                informationService.updatePage(vm.pageId, vm.content).then(function(response) {
                    getPage(vm.pageId);
                    vm.edit = false;
                });
            }
        }

        // tinymce options        
        vm.tinymceOptions = {
            setup: function (editor) {
                editor.on("init", function () {

                });
            },
            branding: false,
            plugins: [
                "advlist autolink lists link image charmap print preview hr anchor pagebreak",
                "searchreplace wordcount visualblocks visualchars code fullscreen",
                "insertdatetime media nonbreaking save table contextmenu directionality",
                "emoticons template paste textcolor colorpicker textpattern imagetools",
                "drive"
            ],
            toolbar1: "insertfile undo redo | styleselect | bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | print preview media | forecolor backcolor emoticons | drive",
            image_advtab: true,
            visualblocks_default_state: true
        };
    }
})(angular);