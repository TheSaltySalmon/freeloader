// Require dependencies
var restify = require('restify');
var db = require('./model/dbService');
var mongoose = require('mongoose');

// Create server
var server = restify.createServer();

// Server options
server.use(restify.acceptParser(server.acceptable));
server.use(restify.queryParser());
server.use(restify.bodyParser());
server.use(restify.CORS());

// Start server
server.listen(8100, function () {
    console.log("Server started @ 8100");
});

// Require routes / endpoints
require('./routes/v1.0/markers')(server);
