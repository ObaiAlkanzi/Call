api = new Api();

function Api() {
    this.chatWindow = function (receiverId,receiverName,profile) {
        $(".chatBox .header span").text(receiverName);
        $(".chatBox .header img").attr("src","\\Profiles\\"+profile);
    }
}