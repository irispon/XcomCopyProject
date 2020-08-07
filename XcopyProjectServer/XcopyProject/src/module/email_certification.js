const nodemailer = require('nodemailer');
const express = require('express');
var router = express.Router();
const ejs = require('ejs');
const request = require('request');
var certification = {};

certification.send = function (app) {


    // 숫자



    const smtpTransport = nodemailer.createTransport({
        service: "Gmail",
        auth: {
            user: "xcopyproject@gmail.com",
            pass: "seven8529741"
        },
        tls: {
            rejectUnauthorized: false
        }
    });
    router.post('/emailAuth', async (req, res) => {
        var email = req.body.email;
        //곧바로 email을 보낼 경우 보안 취약할 수도 있음 암호화 필요
        debug.console('auth request' +email);
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
    app.use('/', router);


    //const options = {
    //    uri: 'http://localhost:4444/emailAuth',
    //    method: 'POST',
    //    form: {
    //        key: 'value',
    //        key: 'value',
    //    }
    //}
    //request.post(options, function (err, httpResponse, body) { /* ... */ })
    //request.post(options, function (err, httpResponse, body) { /* ... */ })
}



module.exports = certification;