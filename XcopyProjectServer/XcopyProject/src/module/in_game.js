

const express = require('express');
const queue = require('./queue');
const { debug } = require('request');
var router = express.Router();

var inGameModule = {};

var emptyRoom=new queue();
var fullRoom;
emptyRoom.enqueue('aaa');
console.log('test: ' + emptyRoom.toString());
inGameModule.connect = function (app,server, database) {
    

    //��Ī �� �����
    router.post('/matching', (req, res) => {
        var id = req.body.id;
        var token = req.body.token;
 




    });
    ///


    var io = require('socket.io')(server);
    //ä�� -�� �ѹ��� �޾ƾ���.
    var chat = io
        .of('/chat')
        .on('connection', function (socket) {
            // chat.emit('a message', { everyone: 'in', '/chat': 'will get' });
            //   socket.emit('a message', { that: 'only', '/chat': 'will get' });
            console.log("__dirname: " + __dirname);

            socket.on('chat', function (data) {

                try {

                    console.debug(data);
                    console.debug(chat.clients.length);
                    chat.emit('chat', data);

                } catch (e) {
                    console.log(e);
                }



            });
            socket.on("disconnect", function () {
                socket.disconnect(0);
            });

        });
     //ä��


}

module.exports = inGameModule;