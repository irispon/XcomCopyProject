
const express = require('express');
const builder = require('@tsdotnet/string-builder');
var router = express.Router();
var login = {};

login.in = function (app, connection) {


    router.post('/login',function (req, res) {

        console.log(req.url);
        var string;
        var json;

        //보안 위험이 존재함.//
        var id ='\''+ req.body.id+'\'';
        var password = '\'' + req.body.password + '\'';
        //보안 위험이 존재함.//
        var sql = 'SELECT id,PASSWORD FROM USER WHERE id= ' + id + ' AND PASSWORD = ' +password

        console.log(sql);

        try {
            connection.query(sql, (error, rows, fields) => {
                if (error) throw error;
                string = JSON.stringify(rows);
                json = JSON.parse(string);

                console.log(json);
                res.send(json);
                res.end();
              
            });
        } catch (e) {
            console.log(e);
        }


    });
    app.use('/', router);
}

module.exports = login;