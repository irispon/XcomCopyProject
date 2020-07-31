var app = require('http').createServer(handler)
var fs = require('fs');

app.listen(4444);

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

// socket.io ��ŸƮ
var io = require('socket.io')(app);

// chat ���ӽ����̽�
var chat = io
    .of('/chat')
    .on('connection', function (socket) {
        chat.emit('a message', { everyone: 'in', '/chat': 'will get' });
        socket.emit('a message', { that: 'only', '/chat': 'will get' });
        console.log("__dirname" + __dirname);
    });

// news ���ӽ����̽�
var news = io
    .of('/news')
    .on('connection', function (socket) {
        socket.emit('item', { news: 'item' });
    });