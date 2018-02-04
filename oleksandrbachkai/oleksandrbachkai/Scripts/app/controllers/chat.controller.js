(function (angular) {
    angular
        .module("app")
        .controller("chatController", chatController);
    chatController.$inject = ["$scope", "$rootScope", "$timeout", "chatService"];
    function chatController($scope, $rootScope, $timeout, $chatService) {
        var vm = this;
        vm.title = "chat";        
        vm.getMessages = getMessages;
        vm.createMessage = createMessage;
        vm.deleteMessage = deleteMessage;
        vm.isUserAdmin = false;
        vm.newMessage = "";

        initialize();

        function initialize() {
            getMessages();

            $timeout(function () {
                $rootScope.$broadcast("adminRequested");
            });

            $scope.$on("adminResponded", function (event, isUserAdmin) {
                vm.isUserAdmin = isUserAdmin;
            });
        }
       
        function getMessages() {
            $chatService.getMessages().then(function (response) {                
                vm.messages = response.data;                
            }, function (response) {
                vm.messages = "";
            });
        }

        function createMessage() {
            $chatService.createMessage(vm.newMessage).then(function (response) {
                getMessages();
            }, function (response) {

            }).finally(function () {
                vm.newMessage = "";
            });
        }

        function deleteMessage(id) {
            $chatService.deleteMessage(id).then(function (response) {
                getMessages();
            }, function (response) {

            })
        };
    }
})(angular);