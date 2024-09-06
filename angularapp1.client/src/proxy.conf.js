const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
    env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'https://localhost:7154';


//route anything starts with /api to backend
const PROXY_CONFIG = [
  {
    context: ['/api'],
    target: target,
    secure: false,
    changeOrigin: true
  }
];

module.exports = PROXY_CONFIG;

module.exports = PROXY_CONFIG;
