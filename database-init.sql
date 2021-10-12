create user arecord_user with password 'arecord_pass';

create database active_record owner arecord_user;

\connect active_record;

create table person (
  person_id   serial primary key,
  first_name  varchar(80) not null,
  last_name   varchar(80),
  birth       date check (birth <= now())
);

create table phrase (
  phrase_id   serial primary key,
  phrase_text varchar(512),
  person_id   integer references person
);