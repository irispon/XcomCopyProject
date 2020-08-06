const crypto = require('crypto');
const nodemailer = require('nodemailer');
const { connect } = require('net');
var create_account = {};

create_account.certification = function (app,connection) {

    app.post('/account', (req, res) => {
        var result="false";
        // 유저 데이터베이스에 존재하는 이메일인지 확인
        try {
            var sql = 'SELECT EXISTS (SELECT id FROM USER WHERE id =\'' + req.body.email + '\')';
            console.log(sql);
            connection.query(sql, (error, rows, fields) => {
                if (error) throw error;
                console.log('', rows);
                result = rows;
            });

            }catch (e) {

            console.log(e);
            res.send('error');
        }

        if (result = 'true') {

            var sql = 'DELETE FROM account WHERE id=?';
            var params = [req.body.email];
            connection.query(sql, params, function (err, rows, fields) {
                if (err) {
                    console.log(err);
                } else {
                    console.log(rows);
                }
            });

            const token = crypto.randomBytes(20).toString('hex');
            console.log("토큰"+token);

            var sql = 'INSERT INTO ACCOUNT VALUES(?,?,?)';
            var par = [req.body.email,req.body.password, token];
            connection.query(sql,par, (error, rows, fields) => {
                if (error) {
                    console.log('', error);
                    
                }
                console.log(rows);
            });


   
            // nodemailer Transport 생성
            const transporter = nodemailer.createTransport({
                service: "Gmail",
                auth: {
                    user: "xcopyproject@gmail.com",
                    pass: "seven8529741"
                },
                tls: {
                    rejectUnauthorized: false
                }
            });
            const emailOptions = { // 옵션값 설정
                from: 'XcopyProject@gmail.com',
                to: req.body.email,
                subject: 'Creat Account Certificate',
                html: 'Please select the following address for authentication'
                    + `local: http://192.168.219.126:4444/account/create/?token=${token}` +
                    ` external: http://122.38.89.43:4444/account/create/?token=${token}`
            };
            transporter.sendMail(emailOptions, res); //전송



            res.send('true');
        }
        else {



            res.send('exist');
        }


    });

    app.get('/account/create', (req, res) => {
        console.log(req.query.token);
        res.render('AccountSucess.html')
        var sql = 'SELECT*FROM ACCOUNT WHERE token =?';
        var par = req.query.token;
        
        connection.query(sql, par, function (err, rows, fields) {
            if (err) {
                console.log(err);
            } else {
   
                
                console.log(rows[0]);
            }
         

            var sql = 'INSERT INTO USER(id, PASSWORD) VALUES(?,?)';
            var par = [rows[0].id, rows[0].password];
            console.log('parameter' + par);
            connection.query(sql, par, function (err, rows, fields) {
                if (err) {
                    console.log(err);
                } else {
                    console.log(rows);
                    json = JSON.stringify(rows);

                }
            });

            var sql = 'DELETE FROM account WHERE token=?';
            var par = req.query.token;
            connection.query(sql, par, function (err, rows, fields) {
                if (err) {
                    console.log(err);
                }
            });

        });

    });


}

module.exports = create_account;