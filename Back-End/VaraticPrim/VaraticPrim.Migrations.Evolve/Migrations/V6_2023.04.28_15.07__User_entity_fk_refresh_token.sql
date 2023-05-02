ALTER TABLE refresh_token
    ADD COLUMN IF NOT EXISTS user_id INT REFERENCES public."user"(id);

ALTER TABLE refresh_token
    DROP COLUMN IF EXISTS email;

ALTER TABLE refresh_token
    RENAME refresh_token_expiration_time TO expiration_time;