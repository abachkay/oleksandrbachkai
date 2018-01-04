(function (angular) {
    angular
        .module("app")
        .controller("indexController", indexController);
    indexController.$inject = ['$scope', '$rootScope', '$timeout', '$cookies', 'indexService'];
    function indexController($scope, $rootScope, $timeout, $cookies, indexService) {
        var vm = this;
        vm.title = 'index';
        vm.pages = [];               
        vm.selectInformationPage = selectInformationPage;        
        vm.createPage = createPage;
        vm.newPageName = 'New page';
        vm.deletePage = deletePage;        

        initialize();

        function initialize() {
            getPages();            
        }
        
        function getPages() {
            indexService.getPages().then(function (response) {
                vm.pages = response.data;                
                if (vm.pages.length !== 0) {                    
                    vm.pageId = vm.pages[0].PageId;                    
                    selectInformationPage(vm.pageId);
                    console.log(vm.pages);
                }
            });
        }        

        function selectInformationPage(pageId)
        {
            vm.pageId = pageId;   
            $timeout(function () {
                $rootScope.$broadcast('pageSelected', vm.pageId);
            });           
        }

        function createPage() {
            indexService.createPage(vm.newPageName).then(function (response) {
                getPages();
                vm.newPageName = 'New page';
            });
        }

        function deletePage(id) {
            indexService.deletePage(id).then(function (response) {
                getPages();                
            });
        }      
    }
})(angular);