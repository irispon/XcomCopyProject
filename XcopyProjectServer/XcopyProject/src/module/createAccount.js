const { cache } = require('ejs');
const crypto = require('crypto');
var create_account = {};

create_account.certification = function (app,connection) {

    app.post('/account', (req, res) => {
        var result="false";
        // email 입력 확인
        if (req.body.email === '') {
            res.status(400).send('email required');
        }
        // 유저 데이터베이스에 존재하는 이메일인지 확인
        try {
            var sql = 'SELECT EXISTS (SELECT id FROM USER WHERE id =\'' + req.body.email + '\')';
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
            const token = crypto.randomBytes(20).toString('hex');
            
            User.findOne(...)
                .then((user) => {
                    const token = crypto.randomBytes(20).toString('hex'); // token 생성
                    const data = { // 데이터 정리
                        token,
                        userId: user.id,
                        ttl: 300 // ttl 값 설정 (5분)
                    };
                    Auth.create(data); // 데이터베이스 Auth 테이블에 데이터 입력
                })

            const nodemailer = require('nodemailer');
            // nodemailer Transport 생성
            const transporter = nodemailer.createTransport({
                service: 'gmail',
                port: 465,
                secure: true, // true for 465, false for other ports
                auth: { // 이메일을 보낼 계정 데이터 입력
                    user: 'test@gmail.com',
                    pass: 'test',
                },
            });
            const emailOptions = { // 옵션값 설정
                from: 'test@gmail.com',
                to: 'user@gmail.com',
                subject: '비밀번호 초기화 이메일입니다.',
                html: '비밀번호 초기화를 위해서는 아래의 URL을 클릭하여 주세요.'
                    + `http://localhost/reset/${token}`,
            };
            transporter.sendMail(emailOptions, res); //전송

            app.post('reset-password', (req, res) => {
                // 입력받은 token 값이 Auth 테이블에 존재하며 아직 유효한지 확인
                Auth.findOne({
                    where: {
                        token: {
                            like: req.body.token
                        },
                        created: {
                            greater: new Date.now() - ttl
                        }
                    }
                }).then((Auth) => { // 유저데이터 호출
                    User.find(...)
                }).then(user) => { // 유저 비밀번호 업데이트
                    User.update(...)
                }
            });

        }
        else {



            res.send('exist');
        }

    });




}