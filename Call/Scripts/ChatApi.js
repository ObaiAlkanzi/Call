chatApi = new ChatApi();
function ChatApi() {
    /*
     * Method for sending Message sendId , ReceiverId, Message
     * Method for get Message
     * Method for Open chat
     * 
     * */
   // var userId = 
    this.senderId =$(".userId").text();
    this.receiverId = 0;
    this.Send = function () {
        var msg = $("#MsgField").val();
        $(".chatBox .body ul").append('<li class="sender"><div></div><h5>' + msg+'</h5></li>');
       // alert(this.senderId + " | " + this.receiverId + " | " + $("#MsgField").val());
        $.connection.chatHub.server.send(this.senderId, this.receiverId,msg);
    };
    this.Receive = function (userId) {
        alert(userId);
    };
    this.OpenChat = function (sender, receiver) {
        alert(sender, receiver);
    };
    this.Users = function (userId) {
        $.get("/Api/Users", {} , function (res) {
            for (var i in res) {
                if (res[i].length > 0) {
                    for (var j in res[i]) {
                        $(".usersBar ul").append("<li id='" + res[i][j]["Id"] + "' onclick='chatApi.setChatBox(" + res[i][j]["Id"] + ",\"" + res[i][j]["Name"] +"\")' >"+res[i][j]["Name"]+" <span></span></li>");
                    }
                    $(".usersBar ul #" + userId).hide();
                }
            }
        });
        
    };

    this.setChatBox = function (receiver, name) {
        $(".chatBox .header h5").text(name);
        this.senderId = userId;
        this.receiverId = receiver;
        
    };
}