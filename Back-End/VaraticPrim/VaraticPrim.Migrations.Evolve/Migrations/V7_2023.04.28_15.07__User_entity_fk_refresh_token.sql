ALTER TABLE ${schema}.refresh_token
    ADD COLUMN IF NOT EXISTS user_id INT REFERENCES ${schema}."user"(id);

ALTER TABLE ${schema}.refresh_token
    DROP COLUMN IF EXISTS email;

ALTER TABLE ${schema}.refresh_token
    RENAME refresh_token_expiration_time TO expiration_time;