CREATE EXTENSION IF NOT EXISTS pgcrypto;

CREATE table person
(
    id serial,
    uuid uuid NOT NULL DEFAULT gen_random_uuid(),
    first_name varchar(40) NOT NULL,
    last_name varchar(40),
    tenant_uuid uuid NOT NULL,

    PRIMARY KEY (id)
);