(function (angular) {
    angular
        .module('app')
        .config(function ($stateProvider, $locationProvider, $urlRouterProvider) {            
            $stateProvider.state('welcome', {
                url: '/',
                templateUrl: '/Content/pages/welcome.html',
                controller: 'welcomeController',
                controllerAs: 'vm'
            }).state('information', {
                url: '/information/{id}',
                templateUrl: '/Content/pages/information.html',
                controller: 'informationController',
                controllerAs: 'vm'
            }).state('files', {
                url: '/files',
                templateUrl: '/Content/pages/files.html',
                controller: 'filesController',
                controllerAs: 'vm'
            }).state('chat', {
                url: '/chat',
                templateUrl: '/Content/pages/chat.html',
                controller: 'chatController',
                controllerAs: 'vm'
            }).state('404', {                   
                templateUrl: '/Content/pages/404.html'
            });          
            $locationProvider.hashPrefix('!');
            $locationProvider.html5Mode(true);
            $urlRouterProvider.otherwise(function ($injector, $location) {
                var state = $injector.get('$state');
                state.go('404');
                return $location.path();
            });
    });   
})(angular);