var app = require('http').createServer(handler)
var fs = require('fs');
var database = require(__dirname + '/config/database_config.js');
var connection = database.init();

app.listen(4444);


try {
  
 
    database.connect(connection);

        
    connection.query('SELECT * from profileresource', (error, rows, fields) => {
        if (error) throw error;
        console.log('User info is: ', rows,fields);
    });
}catch (e) {

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

// socket.io 스타트
var io = require('socket.io')(app);

// chat 네임스페이스
var chat = io
    .of('/chat')
    .on('connection', function (socket) {
       // chat.emit('a message', { everyone: 'in', '/chat': 'will get' });
     //   socket.emit('a message', { that: 'only', '/chat': 'will get' });
        console.log("__dirname: " + __dirname);

        socket.on('chat', function (socket) {

            try {
                console.debug('callback');
                console.debug(socket);
                chat.emit('chat', socket);
            } catch (e) {
                console.log(e);
            }



        });

    });

// news 네임스페이스
var news = io
    .of('/news')
    .on('connection', function (socket) {
        socket.emit('item', { news: 'item' });
    });


