
var http = require('http');
var express = require('express');
var router = express.Router();
var app = express();
var server = http.createServer(app,handler);
var fs = require('fs');
var database = require(__dirname + '/config/database_config.js');
var connection = database.init();
var request = require('request');

var bodyParser = require('body-parser');
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

server.listen(4444);

try {


    database.connect(connection);
    connection.query('SELECT * from profileresource', (error, rows, fields) => {
        if (error) throw error;
        console.log('User info is: ', rows);
    });
} catch (e) {

    console.log(e);
}



function handler(req, res) {

    fs.readFile(__dirname + '/index.html',
        function (err, data) {
            if (err) {
                res.writeHead(500);
                return res.end('Error loading index.html');
            }

            res.writeHead(200);
            res.end(data);
        });


}

app.get('/database', function (req, res) {

    console.log(req.url);
    console.log(req.body);
    try {
        console.log(req.url);
        res.json('test:test');
    } catch (e) {
        console.log(e);
    }

    res.end();

});

app.post('/database', function (req, res) {

    console.log(req.url);
    var string;
    var json;
    console.log(req.body.sql);
    try {
        connection.query(req.body.sql, (error, rows, fields) => {
            if (error) throw error;
            string = JSON.stringify(rows);
            json = JSON.parse(string);
         
            console.log(json);
            res.send(json);
            res.end();

        });
    } catch (e) {
        console.log(e);
    }


});









// socket.io 스타트
var io = require('socket.io')(server);

// chat 네임스페이스
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

// 
//var dbIo = io
//    .of('/database')
//    .on('connection', function (socket) {
//        socket.on('', function (sql) {

//            try {

//                connection.query(sql, (error, rows, fields) => {

//                   var ret = JSON.stringify(rows);
//                    console.log('User info is: ', ret);

//                    socket.emit('database', ret);
//                });

//            } catch (e) {

//                socket.emit('error', e);

//            }
//            socket.on('disconnect', function (data) {

//                try {
//                    console.debug('disconnect' + data);
//                    socket.disconnect(0);

//                } catch (e) {
//                    console.log(e);
//                }



//            });

//        });

//            });





