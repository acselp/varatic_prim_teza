CREATE TABLE service(
    id SERIAL PRIMARY KEY,
    title VARCHAR(255),
    type VARCHAR(255)
);

alter table service
    owner to postgres;