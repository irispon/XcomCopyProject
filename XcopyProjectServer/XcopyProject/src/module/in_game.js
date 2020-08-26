
const express = require('express');
var router = express.Router();

var inGameModule = {};

inGameModule.connect = function (server, database) {

    //��Ī �� �����
    router.post('/emailAuth', async (req, res) => {
        var email = req.body.email;
        //��ٷ� email�� ���� ��� ���� ����� ���� ���� ��ȣȭ �ʿ�
        debug.console('auth request' + email);
        let authNum = Math.random().toString().substr(2, 6);
        let emailTemplete;
        ejs.renderFile('./src/resources/auth/emailtemplete.ejs', { authCode: authNum }, function (err, data) {
            if (err) { console.log('ejs.renderFile err' + err) }
            emailTemplete = data;
        });

        const mailOptions = {

            from: 'ptlsdnjsdnp@gmail.com',
            to: email,
            subject: "XcopyProject Certification Request",
            html: emailTemplete
        };

        await smtpTransport.sendMail(mailOptions, (error, responses) => {

            if (error) {
                res.json({ msg: 'err' });
            } else {
                res.json({ msg: 'sucess' });
            }
            smtpTransport.close();
        });

    });
    ///


    var io = require('socket.io')(server);
    //ä��
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