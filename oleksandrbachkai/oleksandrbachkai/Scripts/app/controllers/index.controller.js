(function (angular) {
    angular
        .module("app")
        .controller("indexController", indexController);
    indexController.$inject = ["$scope", "$rootScope", "$timeout", "$cookies", "loginService", "informationService", "$location"];
    function indexController($scope, $rootScope, $timeout, $cookies, loginService, informationService, $location) {
        var vm = this;
        vm.title = "index";           
        vm.pages = [];               
        vm.selectInformationPage = selectInformationPage;        
        vm.createPage = createPage;
        vm.newPageName = "";
        vm.deletePage = deletePage;
        vm.logout = logout;
        vm.userEmail = null;
        vm.isUserAdmin = false;
        vm.getUserInfo = getUserInfo;

        initialize();

        function initialize() {
            getUserInfo();
            getPages();       

            $scope.$on("loggedIn", function () {
                getUserInfo();
            });

            $scope.$on("adminRequested", function () {
                $rootScope.$broadcast("adminResponded", vm.isUserAdmin);
            });

            // Authorization completation logic.
            var externalAuthParams = {};
            var accountConfirmationParams = {};
            var pairs = $location.url().split(/#|&|\?/);
            for (var i = 0; i < pairs.length; i++) {
                var keyValue = pairs[i].split("=");
                if (keyValue.length == 2) {
                    if (keyValue[0] == "access_token") {
                        externalAuthParams.token = keyValue[1];
                    } else if (keyValue[0] == "email") {
                        externalAuthParams.email = keyValue[1];
                    } else if (keyValue[0] == "userId") {
                        accountConfirmationParams.userId = keyValue[1];
                    } else if (keyValue[0] == "confirmationCode") {
                        accountConfirmationParams.confirmationCode = keyValue[1];
                    }
                }
            }
            if (externalAuthParams.token && externalAuthParams.email) {
                console.log(externalAuthParams);
                loginService
                    .registerExternal(externalAuthParams.token, externalAuthParams.email).then(
                    function (response) {
                        console.log(response);
                        $cookies.put("access_token", response.data.access_token);
                        loginService.getExternalLogins().then(function (response) {
                            location.href = response.data[0].Url;
                        });
                    });
            } else if (externalAuthParams.token) {
                $cookies.put("access_token", externalAuthParams.token);
                $location.path("/");
                getUserInfo();
            }
            if (accountConfirmationParams.userId && accountConfirmationParams.confirmationCode) {
                loginService.confirmEmail(accountConfirmationParams.userId, accountConfirmationParams.confirmationCode)
                    .then(function () {
                        $location.url("/login");
                    });
            }
        }
        
        function getPages() {
            informationService.getPages().then(function (response) {
                vm.pages = response.data;                
                if (vm.pages.length !== 0) {                    
                    vm.pageId = vm.pages[0].PageId;                    
                    selectInformationPage(vm.pageId);                    
                }
            });
        }        

        function selectInformationPage(pageId)
        {
            vm.pageId = pageId;   
            $timeout(function () {
                $rootScope.$broadcast("pageSelected", vm.pageId);                
            });           
        }

        function createPage() {
            informationService.createPage(vm.newPageName).then(function (response) {
                getPages();                
            });
        }

        function deletePage(id) {
            informationService.deletePage(id).then(function(response) {
                getPages();
            });
        }

        function logout() {
            loginService.logout().then(function() {                                
                vm.UserEmail = null;
                vm.isUserAdmin = false;                
                $cookies.put("access_token", undefined);
                $location.path("/");
            });
        }

        function getUserInfo() {
            if ($cookies.get("access_token")) {
                loginService.getUserInfo().then(function (response) {
                    vm.UserEmail = response.data.Email;
                    vm.isUserAdmin = response.data.IsAdministrator;                    
                });
            }           
        }                          
    }
})(angular);