var restify = require('restify');
var db = require('./model/dbService');
var mongoose = require('mongoose');
var server = restify.createServer();
server.use(restify.acceptParser(server.acceptable));
server.use(restify.queryParser());
server.use(restify.bodyParser());
server.use(restify.CORS());
server.listen(8100, function () {
    console.log("Server started @ 8100");
});
require('./routes/v1.0/markers')(server);
