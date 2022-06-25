import http from "k6/http";
import { check, sleep } from 'k6';

export const options = {
    vus: 10,
    duration: '20s',
    // insecureSkipTLSVerify: true,
  };

const take = 2;

// NET 6
const dotnet_npgsql = `https://localhost:44317/api/Films/npgsql?take=${take}`;
const dotnet_npgsql_all = `https://localhost:44317/api/Films/npgsql`;

const dotnet_npgsql_dynamic = `https://localhost:44317/api/Films/npgsql/dynamic?take=${take}`;
const dotnet_npgsql_dynamic_all = `https://localhost:44317/api/Films/npgsql/dynamic`;

const dotnet_text = 'https://localhost:44317/api/text';

// NEST JS
const nest_fastify_pg = `http://localhost:3000/films/pg?take=${take}`;
const nest_fastify_pg_all = `http://localhost:3000/films/pg`;

const nest_text = 'http://localhost:3000/';

// GOLANG

const golang_raw = `http://localhost:8181/films/all/raw?take=${take}`;
const golang_text = 'http://localhost:8181/test';

const golang_gin_raw = `http://localhost:8080/raw?take=${take}`;
const golang_gin_text = 'http://localhost:8080/text';

export default function() {
    const res = http.get(golang_gin_raw);
    check(res, { 'status was 200': (r) => r.status == 200 });
};

// k6 run index.js
// k6 run --vus 10