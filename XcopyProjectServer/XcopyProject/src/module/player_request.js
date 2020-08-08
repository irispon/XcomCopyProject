var express = require('express');
const { cache } = require('ejs');
var player_request = {};
var router = express.Router();


player_request.requests = function (app,connection) {

    app.use('/', router);

    router.post('/getinfo', function (req, res) {

        try {
            var sql = 'SELECT u.id,u.name,u.profile FROM USER AS u JOIN auth au ON u.id = au.id WHERE  au.id = ? AND au.token = ?';
            var par = [req.body.id, req.body.token];
            console.log(par);
            connection.query(sql, par, (error, rows, fields) => {
                console.log(sql);
                var string = JSON.stringify(rows[0]);
                console.log(string);
                res.send(string);
                res.end();

            });
        } catch (e) {

        }


    });
    router.post('/setinfo', function (req, res) {

        try {
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
        } catch (e) {


        }



    });

    router.post('/disconnect', function (req, res) {
        try {
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
                console.log("disconnect"+e);
            }
        } catch (e) {

            console.log("disconnect"+e);
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