(function (angular) {
    angular
        .module("app")
        .controller("informationController", informationController);
    informationController.$inject = ['$scope', '$rootScope', '$cookies'];
    function informationController($scope, $rootScope, $cookies) {
        var vm = this;
        vm.title = 'info';
        vm.content = '';

        // tinymce options
        vm.tinymceModel = 'sdfg';
        vm.tinymceOptions = {
            setup: function (editor) {
                editor.on("init", function () {

                });
            },
            branding: false,
            plugins: [
                'advlist autolink lists link image charmap print preview hr anchor pagebreak',
                'searchreplace wordcount visualblocks visualchars code fullscreen',
                'insertdatetime media nonbreaking save table contextmenu directionality',
                'emoticons template paste textcolor colorpicker textpattern imagetools',
                'drive'
            ],
            toolbar1: 'insertfile undo redo | styleselect | bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image | print preview media | forecolor backcolor emoticons | drive',
            image_advtab: true,           
        };

        $rootScope.$on('pageSelected', function (event, args) {
            vm.content = args.content;
        });

        $rootScope.$on('event1', function (event) {
            alert('event1');
        });      
    }
})(angular);