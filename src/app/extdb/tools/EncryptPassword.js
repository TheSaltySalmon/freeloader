/// <reference path="../../../../typings/cryptojs/cryptojs.d.ts"/>
/// <reference path="../../../../typings/node/node.d.ts"/>
"use strict";
var crypto = require('crypto-js');
var password;
if (process.argv.length > 2) {
    password = process.argv[2];
}
else {
    throw new Error('No password argument given to the script!');
}
var hex2bin = crypto.enc.Hex.parse;
var encrypt = crypto.RC4.encrypt;
var cipherMode = { mode: crypto.mode.ECB, padding: crypto.pad.ZeroPadding };
var text = password + ' encrypted to ';
text += crypto.enc.Base64.stringify(encrypt(password, hex2bin('a8cb61274a9786bbfe797a68b91affef'), cipherMode).ciphertext);
console.log(text);
