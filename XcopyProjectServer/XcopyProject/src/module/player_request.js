var express = require('express');
const { cache } = require('ejs');
var player_request = {};
var router = express.Router();


player_request.requests = function (app,connection) {

    app.use('/', router);

    router.post('/getinfo', function (req, res) {


        var sql = 'SELECT user.id, user.name, user.profile FROM USER, auth WHERE  auth.id = ? AND auth.token = ?';
        var par = [req.body.id, req.body.token];
        connection.query(sql, par, (error, rows, fields) => {

            var string = JSON.stringify(rows[0]);
            console.log(string);
            res.send(string);
            res.end();

        });

    });
    router.post('/setinfo', function (req, res) {

        var sql = 'UPDATE USER u INNER JOIN auth au ON u.id = au.id SET u.name = ?,u.profile =? WHERE u.id = ? AND au.token = ?';
        var par = [req.body.name, req.body.profile, req.body.id, req.body.token];
        try {
            connection.query(sql, par, (error, rows, fields) => {
                if (error)
                    res.send('error');
                console.log(rows);
                res.end();

            });


        } catch (e) {

        }


    });

    router.post('/disconnect', function (req, res) {
        var sql = 'DELETE FROM auth WHERE id=? AND token =?';
        par = [req.body.id, req.body.token];

        try {
            connection.query(sql, par, (error, rows, fields) => {
                if (error)
                    res.send('error');
                console.log(rows);
                res.end();

            });


        } catch (e) {

        }

    });

    router.get('/profiles', function (req, res) {

        console.log('/profiles');
        var sql = 'SELECT * FROM profileresource';
  
            try {
                connection.query(sql, (error, rows, fields) => {
                    if (error)
                        res.send('error');
                    console.log(rows);
                    var string = JSON.stringify(rows);
                    res.send(string);


                });
            }
            catch (e)
            {
             console.log(e);
         }

    });

}

module.exports = player_request;