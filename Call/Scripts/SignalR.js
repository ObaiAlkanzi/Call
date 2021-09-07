/*
 * [-] create signalR methods that generate connections.
 * [1] server - login - send user id to check if already signed in , if n't will return list of users. else will send notification to current signed user.
 * [2] client - unauthorised - receive a string array msg[2] , which Make a notification if another login happened from another device.
 * [3] client - alreadyConnected - have to replace the location to login when login is not authorised.
 * [4] client - login - which receieve dictionary of users and online users if the sign in process is success.
 * [5] client - newClientConnection - receive new connected client data
 * [6] client - disconnectedClient - if any user disconnected this method receive userId
 */
$.connection.hub.start()
    .done(function () {
        $.connection.mainHub.server.login(userId);
    })
    .fail(function () {
        alert("failed to connect");
    });

$.connection.mainHub.client.login = function (data) {
    $.ajax({
        url: '/Api/Users',
        method: 'GET',
        success: function (res) {
            for (var i in res) {
                for (var j in res[i]) {
                    $(".usersBar ul").append("<li id='" + res[i][j]["Id"] + "' onclick='api.chatWindow(" + res[i][j]["Id"] + ",\"" + res[i][j]["Name"] + "\",\"" + res[i][j]["Path"] + "\")' ><img src='\\Profiles\\" + res[i][j]["Path"] + "' />" + res[i][j]["Name"] + "<button value='0'>0</button><span>offline</span></li>");
                }
                for (var i in data) {
                    $("#" + data[i] + " span").text("online");
                    $("#" + data[i]+" span").css({"color":"green"});
                }
                $("#" + userId).hide();
            }
        }
    });
    
}

$.connection.mainHub.client.newClientConnection = function (Id) {
    $("#" + Id + " span").text("online");
    $("#" + Id + " span").css({ "color": "green" });
}

$.connection.mainHub.client.disconnectedClient = function (Id) {
    $("#" + Id + " span").text("offline");
    $("#" + Id + " span").css({ "color": "brown" });
}

$.connection.mainHub.client.unauotherisze = function (res) {
    alert(res);
}

$.connection.mainHub.client.alreadyConnected = function () {
    location.replace("/Home/Logout");
}


$.connection.mainHub.client.receiveMsg = function (senderId,msg) {
    var currentValue = $("#" + senderId + " button").val();
    alert(currentValue);
}