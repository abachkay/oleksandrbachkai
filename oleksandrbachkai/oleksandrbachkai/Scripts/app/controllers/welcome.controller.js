(function (angular) {
    angular
        .module("app")
        .controller("welcomeController", linksController);
    linksController.$inject = ["$scope", "$cookies"];
    function linksController($scope, $cookies, informationService) {
        var vm = this;
        vm.title = "welcome";
        vm.getPage = getPage;

        getPage();

        function getPage() {
            informationService.getPage(3).then(function (response) {                
                vm.content = response.data.Content;
                vm.renderHtml();
            });
        }

        vm.renderHtml = function () {
            return $sce.trustAsHtml(vm.content);
        };

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