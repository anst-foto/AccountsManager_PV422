-- CREATE DATABASE accounts_db;

-- CREATE SCHEMA test;
-- set search_path = "test";

CREATE TABLE table_accounts(
    id SERIAL NOT NULL PRIMARY KEY,
    last_name TEXT NOT NULL,
    first_name TEXT NOT NULL,
    login TEXT NOT NULL,
    password TEXT NOT NULL
);