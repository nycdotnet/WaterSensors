CREATE TABLE reading(
    id BIGSERIAL NOT NULL PRIMARY KEY,
    sensor_id text NOT NULL,
    timestamp timestamptz NOT NULL,
    temperature real NULL,
    pressure real NULL,
    ph real NULL
);
