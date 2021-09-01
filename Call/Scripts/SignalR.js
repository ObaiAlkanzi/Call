
$.connection.hub.start()
    .done(function () {
        chatApi.Users(userId);
        $.connection.usersHub.server.userLogin(userId);
        $.connection.usersHub.server.onlineUsers();
    })
    .fail(function () { alert("failed to connect"); });

$.connection.usersHub.client.newUser = function (data) {
    $(".usersBar ul").append("<li class='newUser' id='" + data["Id"] + "'>" + data["Name"] + "<span></span></li>");
    setTimeout(function () {
        $(".usersBar ul li").removeAttr("class");
    }, 3000);
}

$.connection.usersHub.client.connectedUser = function (id) {
    $("#" + id + " span").css({ "background": "green" });
}

$.connection.serverHub.client.disconnected = function (id) {
    $("#" + id + " span").css({ "background": "lightgray" });
}

$.connection.usersHub.client.onlineUsers = function (data) {
    for (var i in data) {
        //alert(data[i]);
        $("#" + data[i] + " span").css({ "background": "green" });
    }
}
$.connection.chatHub.client.receive = function (sender,msg) {
    $(".chatBox .body ul").append('<li class="receiver"><div></div><h5>' + msg + '</h5></li>');
}