create table contact
(
    id             serial
        primary key,
    first_name     varchar(50) not null,
    last_name      varchar(50) not null,
    phone          varchar(20) not null,
    mobile         varchar(20) not null,
    created_on_utc timestamp default now(),
    updated_on_utc timestamp default now()
);

alter table contact
    owner to postgres;

create table "user"
(
    id             serial
        primary key,
    email          varchar not null
        unique,
    password_hash  varchar not null,
    role_id        integer not null,
    password_salt  varchar(255),
    created_on_utc timestamp,
    updated_on_utc timestamp,
    contact_id     integer
        constraint contact_id_constr
            references contact
);

alter table "user"
    owner to postgres;

create table location
(
    id             serial
        primary key,
    address        varchar(255),
    postal_code    varchar(10),
    bloc           varchar(50),
    apartment      varchar(50),
    nr_pers        integer,
    user_id        integer,
    created_on_utc timestamp,
    updated_on_utc timestamp
);

alter table location
    owner to postgres;

create table counter
(
    id             serial
        primary key,
    barcode        varchar(255),
    value          integer,
    location_id    integer,
    created_on_utc timestamp,
    updated_on_utc timestamp
);

alter table counter
    owner to postgres;

