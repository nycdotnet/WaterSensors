import http from 'k6/http';
import { check, sleep } from 'k6';
// import { exec } from 'k6/execution';

export const options = {
    scenarios: {
        "basic": {
            exec: "basicLoadTest",
            vus: 100,
            executor: 'constant-vus',
            duration: '30s'
        }
    }
};

const defaultHeaders = { 'Content-Type': 'application/json', 'accept': '*/*' };

export function basicLoadTest() {
    const reading = {
        sensorId: 'sensor1',
        timestamp: new Date(),
        temperature: randomBetween(19, 27),
        pressure: randomBetween(35, 38),
        pH: randomBetween(6, 8)
    };

    const payload = JSON.stringify(reading);

    const result = http.post(
        "https://localhost:7214/Readings",
        payload,
        { headers: defaultHeaders });

    check(result, { 'status was 200': r => r.status == 200 });
    sleep(0.5);
}

const randomBetween = (min, max) => (Math.random() * (max + 1 - min)) + min;
