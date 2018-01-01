(function (angular) {
    angular
        .module("app")
        .controller("indexController", indexController);
    indexController.$inject = ['$scope', '$rootScope', '$cookies', 'indexService'];
    function indexController($scope, $rootScope, $cookies, indexService) {
        var vm = this;
        vm.title = 'index';

        initialize();
       
        vm.selectInformationPage = selectInformationPage;        

        function initialize() {
            getPages();
            selectInformationPage(0);
        }
        
        function getPages() {
            indexService.getPages().then(function (response) {
                vm.pages = response.data;
                vm.pageIndex = 0;
                vm.pageId = vm.pages[vm.pageIndex].PageId;
                vm.pageName = vm.pages[vm.pageIndex].Name;
                vm.pageContent = vm.pages[vm.pageIndex].Content;
                console.log(vm.pages);
            });
        }        

        function selectInformationPage(pageIndex)
        {
            vm.pageIndex = pageIndex;
            $rootScope.$broadcast('event1');
            $rootScope.$broadcast('pageSelected', {
                content: vm.pageContent
            });            
        }
        
        indexService.getPages()

        $rootScope.$broadcast('event1');      
    }
})(angular);