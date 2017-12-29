(function (angular) {
    angular
        .module("app")
        .controller("indexController", indexController);
    indexController.$inject = ['$scope', '$cookies', '$location'];
    function indexController($scope, $cookies, $location) {
        var vm = this;
        vm.title = 'index';
        var tokenParameter = $location.url().match(/\#(?:access_token)\=([\S\s]*?)\&/);
        if (tokenParameter) {
            var token = tokenParameter[1];
            if (token) {
                console.log(token);
                $cookies.put('access_token', token);
            }    
        }
        
    }
})(angular);