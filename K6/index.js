import http from "k6/http";
import { check, sleep } from 'k6';

export const options = {
    vus: 30,
    duration: '10s',
    // insecureSkipTLSVerify: true,
  };

const net_npgsql = 'https://localhost:44317/api/Films/npgsql?take=2';
const net_text = 'https://localhost:44317/api/text';

export default function() {
    const res = http.get(net_npgsql);
    check(res, { 'status was 200': (r) => r.status == 200 });
};

// k6 run index.js
// k6 run --vus 10