///<reference path="../../../../typings/cryptojs/cryptojs.d.ts"/>
///<reference path="../../../../typings/node/node.d.ts"/>

import * as crypto from 'crypto-js';

let password: string;
if (process.argv.length > 2) {
    password = process.argv[2];
} else {
    throw new Error('No password argument given to the script!');
}

let hex2bin = crypto.enc.Hex.parse;
let encrypt = crypto.RC4.encrypt;
let cipherMode = { mode: crypto.mode.ECB, padding: crypto.pad.ZeroPadding };

let text: string = password + ' encrypted to ';
text += crypto.enc.Base64.stringify(encrypt(password, hex2bin('a8cb61274a9786bbfe797a68b91affef'), cipherMode).ciphertext);

console.log(text);
