const express = require("express");
const util = require('util');
const exec = util.promisify(require('child_process').exec);

const app = express();

app.get("/", async function (req, res) {
    const { stdout: ip } = await exec('hostname -i');
    const { stdout: proccesses } = await exec('ps -ax');
    const { stdout: spaceAvailable } = await exec('df');
    const { stdout: timeSinceBoot } = await exec('uptime');

    const response = await fetch("http://service-2:8080/")
    const data = await response.json();

    return res.send({
        service_2: data,
        service_1: {
            ip: ip.trim(),
            proccesses: proccesses.split("\n").map(p => p.trim()).filter(p => p.length !== 0),
            spaceAvailable: spaceAvailable.split("\n").map(p => p.trim()).filter(p => p.length !== 0),
            timeSinceBoot: timeSinceBoot.trim()
        }
    });
});

app.listen(3000, function () {
    console.log('Listening on port 3000');
});