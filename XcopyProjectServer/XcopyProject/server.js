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

        socket.on('chat', function (data) {

            try {
                console.debug('callback');
                console.debug(data);
                chat.emit('chat', data);
            } catch (e) {
                console.log(e);
            }



        });
        socket.on('disconnect', function (data) {

            try {
                console.debug('disconnect' + data);
                socket.disconnect();

            } catch (e) {
                console.log(e);
            }



        });

    });

// news 네임스페이스
var dbIo = io
    .of('/database')
    .on('connection', function (socket) {
        socket.on('', function (sql) {

            try {

                connection.query(sql, (error, rows, fields) => {

                    console.log('User info is: ', rows, fields);
                    socket.emit('dataBase', rows);
                });

            } catch (e) {

                socket.emit('error', e);

            }
            socket.on('disconnect', function (data) {

                try {
                    console.debug('disconnect' + data);
                    socket.disconnect();

                } catch (e) {
                    console.log(e);
                }



            });

        });

            });



