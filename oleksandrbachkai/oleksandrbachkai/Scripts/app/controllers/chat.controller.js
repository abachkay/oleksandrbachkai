(function (angular) {
    angular
        .module("app")
        .controller("chatController", chatController);
    chatController.$inject = ["$scope","chatService"];
    function chatController($scope, $chatService) {
        var vm = this;
        vm.title = "chat";        
        vm.getMessages = getMessages;
        vm.createMessage = createMessage;
        vm.deleteMessage = deleteMessage;
        vm.isUserAdmin = false;
        vm.newMessage = "";

        init();

        function init() {
            getMessages();
            $scope.$on("userChanged", function (event, isUserAdmin) {
                vm.isUserAdmin = isUserAdmin;
                console.log(isUserAdmin);
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