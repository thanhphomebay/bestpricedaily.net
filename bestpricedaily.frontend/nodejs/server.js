var express = require('express');
var app = express();
var fs = require("fs");
var path = require('path');
var request = require("request");
const bodyParser = require('body-parser')
app.use(bodyParser.urlencoded({ extended: false }))
app.use(bodyParser.json());

const config = require('./config');
const configuration = config.getConfig();




// app.use(bodyParser);
app.use(function (req, res, next) {
	var allowedOrigins = ['http://127.0.0.1:4200', 'http://localhost:4200', 'http://192.168.1.150:4200'];
	var origin = req.headers.origin;
	if (allowedOrigins.indexOf(origin) > -1) {
		res.setHeader('Access-Control-Allow-Origin', origin);
	}
	res.setHeader('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE');
	res.setHeader('Access-Control-Allow-Headers', 'X-Requested-With,content-type');
	res.setHeader('Access-Control-Allow-Credentials', true);

	next();
});

function getAccessToken(cb) {

	var url = configuration.ACCESS_TOKEN_URL;
	var token = configuration.CLIENT_ID + ":" + configuration.SECRET,
		encodedKey = new Buffer(token).toString('base64'),
		payload = "grant_type=client_credentials&Content-Type=application%2Fx-www-form-urlencoded&response_type=token&return_authn_schemes=true",
		headers = {
			'authorization': "Basic " + encodedKey,
			'accept': "application/json",
			'accept-language': "en_US",
			'cache-control': "no-cache",
			'content-type': "application/x-www-form-urlencoded",
			// 'PayPal-Partner-Attribution-Id': configuration.BN_CODE
		}

	var options = {
		method: 'POST',
		url: configuration.ACCESS_TOKEN_URL,
		headers: {
			'authorization': "Basic " + encodedKey,
			'accept': "application/json",
			'accept-language': "en_US",
			'cache-control': "no-cache",
			'content-type': "application/x-www-form-urlencoded",
			// 'PayPal-Partner-Attribution-Id': configuration.BN_CODE

		},
		body: payload
	}
	console.log(options);
	request(options, function (error, response, body) {
		if (error) {
			throw new Error(error);
		}
		else {
			cb(body)
		}
	});
}
function buildCreatePaymentPayload(data) {
	let order = {
		"intent": "CAPTURE",
		"purchase_units": [
			{
				"amount": {
					"currency_code": "USD", //from server
					"value": "7",//from server
					"breakdown": {
						"item_total": { "value": "7", "currency_code": "USD" } //from server
					}
				},
				"items": [],

			}
		],
		"application_context": {
			"return_url": "http://127.0.0.1/success",
			"cancel_url": "http://127.0.0.1/err",
			"brand_name": "AKAZ",
			// "locale": "fr-FR",
			"user_action": "CONTINUE"
		},
	};

	data[0].items.forEach(item => {
		aux = {
			"name": "Hafer", //from server
			"unit_amount": { "value": item.unit_amount.value, "currency_code": "USD" },
			"quantity": item.quantity,
			"sku": item.sku
		};
		order.purchase_units[0].items.push(aux);
	})
	console.log("create order: " + JSON.stringify(order));
	return order;
}
// return {
// 	"intent": "CAPTURE",
// 	"purchase_units": [
// 		{
// 			"amount": {
// 				"currency_code": "USD",
// 				"value": "100.00",
// 				"breakdown": {
// 					"item_total": { "value": "100", "currency_code": "USD" }
// 				  }
// 			},
// 			"items": [{
// 				"name": "Hafer",
// 				"unit_amount": { "value": "100", "currency_code": "USD" },
// 				"quantity": "1",
// 				"sku": "haf001"
// 			}]
// 		}
// 	]
// }
var server = app.listen(80, function () {
	var host = server.address().address
	var port = server.address().port
	console.log("Example app listening at http://%s:%s", host, port)
})
function uuidv4() {
	return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
		var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
		return v.toString(16);
	});
}

app.get('/success', function (req, res, next) {
	console.log('success');
	res.send("success redirect")
});
app.post('/createorder', function (req, res, next) {
	console.log("body: " + req.body.purchase_units);
	try {

		var payLoad = buildCreatePaymentPayload(req.body.purchase_units);
		// var payLoad = {
		// 	"intent": "capture",
		// 	"payer": {
		// 		"payment_method": "paypal"
		// 	},
		// 	"redirect_urls": {
		// 		"return_url": "http://128.0.0.1:3000/success",
		// 		"cancel_url": "http://127.0.0.1:3000/err"
		// 	},
		// 	"transactions": [{
		// 		"amount": {
		// 			"total": 39.00,
		// 			"currency": "USD"
		// 		},
		// 		"description": " a book on mean stack "
		// 	}]
		// }

		getAccessToken(function (data) {

			var accessToken = JSON.parse(data).access_token;

			var options = {
				method: 'POST',
				url: configuration.CREATE_PAYMENT_URL,
				headers: {
					'content-type': "application/json",
					'authorization': "Bearer " + accessToken,
					'cache-control': "no-cache",
					// 'PayPal-Partner-Attribution-Id': configuration.BN_CODE,
					// 'PayPal-Client-Metadata-Id': req.body.riskParingId
				},
				body: payLoad,
				json: true

			}

			request(options, function (error, response, body) {
				if (error) {
					console.log(body);
					throw new Error(error);
				}
				else {

					console.log(body);
					console.log(options.headers);
					res.send(body);

				}
			});

		});
	} catch (e) {
		console.log(e)
	}
});
