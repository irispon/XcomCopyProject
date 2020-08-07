
var certification = require('./src/module/email_certification');
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


server.listen(4444);

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));
app.set('views', __dirname + '/views');
app.set('view engine', 'ejs');
app.engine('html', require('ejs').renderFile);

try {


    database.connect(connection);
    //connection.query('SELECT * FROM profileresource', (error, rows, fields) => {
    //    if (error) throw error;
    //    console.log('User info is: ', rows);
    //});
} catch (e) {

    console.log(e);
}



function handler(req, res) {


    response.writeHead(200, { 'Content-Type': 'text/html;charset=UTF-8' });
    console.log('test' + __dirname + '/index.html');
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

app.get('/', function (req, res) {
    res.render('index.html');
    res.send(true);

});
//데이터 베이스 정보 요청 코드
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
//데이터 베이스 정보 요청 코드



// socket.io 스타트
var io = require('socket.io')(server);

// 채팅방//
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
// 채팅방//

///이메일 인증
try {
    certification.send(app);
}
catch (e) {

    console.log(e);
}
///이메일 인증
var login = require('./src/module/login');
try {
    login.in(app, connection);

} catch (e) {

    console.log("login error: " + e);
}

///이메일 인증
//가입 인증
var create_account = require('./src/module/create_account');
try {
    create_account.certification(app, connection);

} catch (e) {

    console.log("login error: " + e);
}
//가입인증