CREATE INDEX IF NOT EXISTS ix_reading_sensor_timestamp on public.reading (sensor_id, "timestamp");

CREATE INDEX IF NOT EXISTS ix_reading_timestamp_sensor on public.reading ("timestamp", sensor_id);