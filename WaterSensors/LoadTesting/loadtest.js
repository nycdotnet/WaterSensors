import http from 'k6/http';
import { check, sleep } from 'k6';
import exec from 'k6/execution';

export const options = {
    scenarios: {
        "basic": {
            exec: "basicLoadTest",
            vus: 2000,
            executor: 'constant-vus',
            duration: '60s'
        },
    }
};

const defaultHeaders = { 'Content-Type': 'application/json', 'accept': '*/*' };

export function basicLoadTest() {
    const result = http.post(
        "https://localhost:7214/Readings",
        generateReadingJson(),
        { headers: defaultHeaders });

    check(result, { 'status was 200': r => r.status == 200 });
    sleep(0.5);
}

const generateReadingJson = () => JSON.stringify({
    sensorId: `sensor${exec.vu.idInInstance}`,
    timestamp: new Date(),
    temperature: randomBetween(45, 65),
    pressure: randomBetween(20, 90),
    pH: randomBetween(5, 9)
});

const randomBetween = (min, max) => ((Math.random() * (max + 1 - min)) + min).toFixed(2);

