
const express = require('express');
const builder = require('@tsdotnet/string-builder');
const crypto = require('crypto');
var router = express.Router();
var login = {};

login.in = function (app, connection) {


    router.post('/login',function (req, res) {

        console.log(req.url);



       // var sql = 'SELECT id,PASSWORD FROM USER WHERE id= ' + id + ' AND PASSWORD = ' +password
        var sql = 'SELECT EXISTS (SELECT id,PASSWORD FROM USER WHERE id =? AND PASSWORD =?)';
        var par = [req.body.id, req.body.password];
        console.log(sql+"  " +par);

        try {
            connection.query(sql, par, (error, rows, fields) => {
                if (error) throw error;
                console.log(rows);
                var string = JSON.stringify(rows);
                var json = JSON.parse(string);
                var firstKey = Object.keys(json[0])[0];
                const token = crypto.randomBytes(20).toString('hex');

                var request = { id: json[0][firstKey], token: token };
           
         

                //json = JSON.parse(string);

                console.log(request); 
                res.send(request);
              
                
                res.end();
                try {
                    if (json[0][firstKey] == '1') {
                        console.log("아이디 존재");
                        var sql = 'DELETE FROM auth WHERE id=?';
                        var params = [req.body.id];
                        connection.query(sql, params, (error, rows, fields) => {
                            if (error) {


                            }
                          

                        
                        var sql = 'INSERT INTO auth(id,token) VALUES (?,?)';
                        var par = [req.body.id, token];
                        connection.query(sql, par, (error, rows, fields) => {
                            if (error) throw error;
                            console.log('token upload');

                        });
                        });
                    }
                } catch (e) {


                    console.log("error"+e);
                }



            });
        } catch (e) {
            console.log(e);
            res.end();
        }


    });
    app.use('/', router);
}

module.exports = login;