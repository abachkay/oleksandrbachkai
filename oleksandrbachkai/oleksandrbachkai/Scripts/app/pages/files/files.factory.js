(function (angular) {
    angular
        .module("app")
        .factory("filesFactory", filesFactory);
    filesFactory.$inject = ["$http"];
    function filesFactory($http) {
        return {
            getValues: getValues
        }       
        function getValues(token) {
            return $http({
                method: "GET",
                url: "/api/values",
                headers: {
                    "Authorization": "Bearer " + token
                }
            });
        }                
    }
})(angular);