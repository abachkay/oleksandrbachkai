(function (angular) {
    angular
        .module("app")
        .factory("filesFactory", filesFactory);
    filesFactory.$inject = ["$http"];
    function filesFactory($http) {
        return {
            getValues: getValues
        }       
        function getValues() {
            return $http({
                method: "GET",
                url: "/api/values",                
            });
        }                
    }
})(angular);