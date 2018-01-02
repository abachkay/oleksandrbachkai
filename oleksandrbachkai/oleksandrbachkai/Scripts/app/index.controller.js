(function (angular) {
    angular
        .module("app")
        .controller("indexController", indexController);
    indexController.$inject = ['$scope', '$rootScope', '$timeout', '$cookies', 'indexService'];
    function indexController($scope, $rootScope, $timeout, $cookies, indexService) {
        var vm = this;
        vm.title = 'index';

        initialize();
       
        vm.selectInformationPage = selectInformationPage;        

        function initialize() {
            getPages();            
        }
        
        function getPages() {
            indexService.getPages().then(function (response) {
                vm.pages = response.data;                
                if (vm.pages.length !== 0) {
                    vm.pageIndex = 0;
                    vm.pageId = vm.pages[vm.pageIndex].PageId;
                    vm.pageName = vm.pages[vm.pageIndex].Name;
                    vm.pageContent = vm.pages[vm.pageIndex].Content;
                    selectInformationPage(vm.pageIndex);
                    console.log(vm.pages);
                }
            });
        }        

        function selectInformationPage(pageIndex)
        {
            vm.pageIndex = pageIndex;   
            $timeout(function () {
                $rootScope.$broadcast('pageSelected', vm.pageContent);
            });           
        }                    
    }
})(angular);