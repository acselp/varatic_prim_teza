CREATE TABLE ${schema}.invoice (
    id SERIAL PRIMARY KEY,
    location_id INTEGER 
        CONSTRAINT location_id_constr 
            REFERENCES ${schema}.location(id),
    service_id INTEGER
        CONSTRAINT service_id_constr
            REFERENCES ${schema}.service(id),
    payment_status BOOLEAN,
    amount INTEGER,
    data JSON,
    created_on_utc timestamp,
    updated_on_utc timestamp
)