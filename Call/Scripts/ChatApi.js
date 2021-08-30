chatApi = new ChatApi();
function ChatApi() {
    /*
     * Method for sending Message sendId , ReceiverId, Message
     * Method for get Message
     * Method for Open chat
     * 
     * */
   
    this.Send = function (sender, receiver, message) {
        alert(sender + receiver + message);
    };
    this.Receive = function (userId) {
        alert(userId);
    };
    this.OpenChat = function (sender, receiver) {
        alert(sender, receiver);
    };
    this.Users = function () {
        
        $.get("/Api/Users", {} , function (res) {
            for (var i in res) {
                if (res[i].length > 0) {
                    for (var j in res[i]) {
                        $(".usersBar ul").append("<li id='" + res[i][j]["Id"] +"'>"+res[i][j]["Name"]+"</li>");
                    }
                    
                } else {
                    $(".usersBar ul").append("<li>No Users</li>");
                }
            }
        });
        
    };

}