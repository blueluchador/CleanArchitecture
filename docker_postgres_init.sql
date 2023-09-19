CREATE EXTENSION IF NOT EXISTS pgcrypto;

CREATE table hello
(
    id serial,
    uuid uuid NOT NULL DEFAULT gen_random_uuid(),
    name varchar(40) NOT NULL,

    PRIMARY KEY (id)
);