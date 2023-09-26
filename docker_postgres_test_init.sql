EXEC SQL INCLUDE docker_postgres_init.sql;

-- Seed test data
INSERT INTO person(uuid, first_name, last_name) VALUES ('b5d74ff1-572f-4dd5-beb3-3aa67adf6b49', 'Gerardo', 'Gastelum')