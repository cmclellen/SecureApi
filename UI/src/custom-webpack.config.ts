import { EnvironmentPlugin } from 'webpack';
const Dotenv = require('dotenv-webpack');

var dotenvPath = `./.env`;
var nodeEnv = process.env['NODE_ENV'];
if(!!nodeEnv) {
  dotenvPath += `.${process.env['NODE_ENV']}`;
}

console.log(dotenvPath);
module.exports = {
  plugins: [new Dotenv({ path: dotenvPath })],
};