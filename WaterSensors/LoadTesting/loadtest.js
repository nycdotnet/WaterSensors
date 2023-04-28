import http from 'k6/http';
import { check, sleep } from 'k6';
import exec from 'k6/execution';

export const options = { scenarios: {} };

const ingestScenario = {
    exec: "ingestLoadTest",
    vus: 200,
    executor: 'constant-vus',
    duration: '30s'
};

const sensorTimelineScenario = {
    exec: "sensorTimelineLoadTest",
    executor: 'ramping-vus',
    startVUs: 0,
    stages: [
        { duration: '20s', target: 30 },
        { duration: '10s', target: 0 },
    ],
    gracefulRampDown: '0s',
}

if (__ENV.SCENARIO === 'ingestLoadTest') {
    options.scenarios['ingestLoadTest'] = ingestScenario;
}
else if (__ENV.SCENARIO === 'sensorTimelineLoadTest') {
    options.scenarios['sensorTimelineLoadTest'] = sensorTimelineScenario;
}
else {
    throw new Error('Must specify a supported scenario.');
}

const defaultHeaders = { 'Content-Type': 'application/json', 'accept': 'application/json' };

export function ingestLoadTest() {
    const result = http.post(
        "https://localhost:7214/Readings",
        generateReadingJson(),
        { headers: defaultHeaders });

    check(result, { 'status was 200': r => r.status == 200 });
    sleep(0.5);
}

export function sensorTimelineLoadTest() {
    const result = http.get(
        `https://localhost:7214/Sensors?SensorId=sensor${exec.vu.idInInstance}`,
        generateReadingJson(),
        { headers: defaultHeaders });

    check(result, { 'status was 200': r => r.status == 200 });
    sleep(0.1);
}

const generateReadingJson = () => JSON.stringify({
    sensorId: `sensor${exec.vu.idInInstance}`,
    timestamp: new Date(),
    temperature: randomBetween(45, 65),
    pressure: randomBetween(20, 90),
    pH: randomBetween(5, 9),
    latitude: randomBetween(-89.99, 90),
    longitude: randomBetween(-179.99, 180)
});

const randomBetween = (min, max) => ((Math.random() * (max + 1 - min)) + min).toFixed(2);

