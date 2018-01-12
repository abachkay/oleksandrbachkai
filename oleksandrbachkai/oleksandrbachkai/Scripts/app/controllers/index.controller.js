(function (angular) {
    angular
        .module("app")
        .controller("indexController", indexController);
    indexController.$inject = ["$scope", "$rootScope", "$timeout", "$cookies", "loginService", "informationService", "$location", "$state"];
    function indexController($scope, $rootScope, $timeout, $cookies, loginService, informationService, $location, $state) {
        var vm = this;
        vm.title = "index";   
        vm.title = "index";
        vm.pages = [];               
        vm.selectInformationPage = selectInformationPage;        
        vm.createPage = createPage;
        vm.newPageName = "New page";
        vm.deletePage = deletePage;
        vm.logout = logout;
        vm.userEmail = null;
        vm.isUserAdmin = false;
        vm.getUserInfo = getUserInfo;

        initialize();        

        function initialize() {
            getUserInfo();
            getPages();
            selectInformationPage();
        }

        function getPages() {
            informationService.getPages().then(function(response) {
                vm.pages = response.data.filter(function(d) { return d.PageId !== 3 });                
            });
        }

        function selectInformationPage(pageId) {
            $timeout(function () {
                if (pageId) {
                    if (pageId === 3) {
                        $state.go("welcome");
                    } else {
                        $state.go("information({pageId: " + pageId + "})");
                    }                    
                }
                var pathParts = $location.path().split("/");
                if ($location.path() === "/") {
                    vm.pageId = 3;
                } else if (pathParts.length === 3 && pathParts[1].toLowerCase() === "information") {
                    vm.pageId = pathParts[2];
                }
                console.log(vm.pageId);
                $rootScope.$broadcast("pageSelected", vm.pageId);
            });
        }

        function createPage() {
            informationService.createPage(vm.newPageName).then(function (response) {
                getPages();
                vm.newPageName = "New page";
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

        $scope.$on("loggedIn", function () { getUserInfo(); });
           
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
                    loginService.getExternalLogins().then(function(response) {
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
})(angular);