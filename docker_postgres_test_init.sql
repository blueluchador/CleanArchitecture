CREATE EXTENSION IF NOT EXISTS pgcrypto;

CREATE table person
(
    id serial,
    uuid uuid NOT NULL DEFAULT gen_random_uuid(),
    first_name varchar(40) NOT NULL,
    last_name varchar(40),

    PRIMARY KEY (id)
);

-- Seed test data
INSERT INTO person(uuid, first_name, last_name) VALUES ('b5d74ff1-572f-4dd5-beb3-3aa67adf6b49', 'Gerardo', 'Gastelum')