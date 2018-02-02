(function (angular) {
    angular
        .module("app")
        .factory("chatService", chatService);

    chatService.$inject = ["$http", "$cookies"];

    function chatService($http, $cookies) {
        return {
            getMessages: getMessages,                       
            createMessage: createMessage,
            deleteMessage: deleteMessage
        }         

        function getMessages() {
            return $http({
                method: "GET",
                url: "/api/messages",
                headers: { "Authorization": "Bearer " + $cookies.get("access_token") }
            });
        }

        function createMessage(text) {
            return $http({
                method: "POST",
                url: "/api/messages",
                data: { Text = text },
                headers: { "Authorization": "Bearer " + $cookies.get("access_token") }
            });
        }

        function deleteMessage(id) {
            return $http({
                method: "DELETE",
                url: "/api/messages/" + id,
                headers: { "Authorization": "Bearer " + $cookies.get("access_token") }
            });
        }          
    }
})(angular);