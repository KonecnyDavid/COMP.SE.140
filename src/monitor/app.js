const express = require("express");
const util = require('util');
const exec = util.promisify(require('child_process').exec);

const app = express();

app.get("/", async function (req, res) {
    await exec('docker stop $(docker ps -a -q)');
    return res.send("OK");
});

app.listen(3000, function () {
    console.log('Listening on port 3000');
});