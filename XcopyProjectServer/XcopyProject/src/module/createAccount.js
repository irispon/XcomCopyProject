const { cache } = require('ejs');
const crypto = require('crypto');
var create_account = {};

create_account.certification = function (app,connection) {

    app.post('/account', (req, res) => {
        var result="false";
        // email �Է� Ȯ��
        if (req.body.email === '') {
            res.status(400).send('email required');
        }
        // ���� �����ͺ��̽��� �����ϴ� �̸������� Ȯ��
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
                    const token = crypto.randomBytes(20).toString('hex'); // token ����
                    const data = { // ������ ����
                        token,
                        userId: user.id,
                        ttl: 300 // ttl �� ���� (5��)
                    };
                    Auth.create(data); // �����ͺ��̽� Auth ���̺� ������ �Է�
                })

            const nodemailer = require('nodemailer');
            // nodemailer Transport ����
            const transporter = nodemailer.createTransport({
                service: 'gmail',
                port: 465,
                secure: true, // true for 465, false for other ports
                auth: { // �̸����� ���� ���� ������ �Է�
                    user: 'test@gmail.com',
                    pass: 'test',
                },
            });
            const emailOptions = { // �ɼǰ� ����
                from: 'test@gmail.com',
                to: 'user@gmail.com',
                subject: '��й�ȣ �ʱ�ȭ �̸����Դϴ�.',
                html: '��й�ȣ �ʱ�ȭ�� ���ؼ��� �Ʒ��� URL�� Ŭ���Ͽ� �ּ���.'
                    + `http://localhost/reset/${token}`,
            };
            transporter.sendMail(emailOptions, res); //����

            app.post('reset-password', (req, res) => {
                // �Է¹��� token ���� Auth ���̺� �����ϸ� ���� ��ȿ���� Ȯ��
                Auth.findOne({
                    where: {
                        token: {
                            like: req.body.token
                        },
                        created: {
                            greater: new Date.now() - ttl
                        }
                    }
                }).then((Auth) => { // ���������� ȣ��
                    User.find(...)
                }).then(user) => { // ���� ��й�ȣ ������Ʈ
                    User.update(...)
                }
            });

        }
        else {



            res.send('exist');
        }

    });




}