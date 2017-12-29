(function (angular) {
    angular
        .module("app")
        .factory("filesService", loginService);
    loginService.$inject = ["$http"];
    function loginService($http) {
        return {
            getInfo: getInfo            
        }       
        function getInfo() {
            return $http({
                method: "GET",
                url: "/api/values"              
            });
        }      
    }
})(angular);