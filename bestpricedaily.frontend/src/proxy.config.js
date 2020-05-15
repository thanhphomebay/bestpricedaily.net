const PROXY_CONFIG = [
    {
        context: [
            "/api",
            "/datachannel",
            "/img",
        ],
        target: "http://192.168.1.150:45455",
        // target: "http://192.168.1.150:5000",
        //target: "http://localhost:5000",
        secure: false,
        logLevel: 'debug',
        ws: true
    }
]

module.exports = PROXY_CONFIG;
