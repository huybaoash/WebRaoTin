﻿$(function () {


    var chatHub = $.connection.chat;
    loadClient(chatHub);
    //khởi chạy connect hub
    $.connection.hub.start().done(function () {


        $('#btnSend').click(function () {
            var msg = $('#txtMessage').val();
            var name = $('#hName').val();
            chatHub.server.message(name, msg);
            $('#txtMessage').val('').focus();
        });
    });

});

// load các ham bên phía client
function loadClient(chatHub) {
    
    chatHub.client.message = function (name, msg) {
        $('#contentMsg').before("<li><a><strong>" + name + "</strong>: " + msg + "</a></li>");
    }
    chatHub.client.connect = function (name) {
        $('#hName').val(name);
    }
}

function moForm() {
    document.getElementById("myForm").style.display = "block";
}
/*Hàm Đóng Form*/
function dongForm() {
    document.getElementById("myForm").style.display = "none";
}