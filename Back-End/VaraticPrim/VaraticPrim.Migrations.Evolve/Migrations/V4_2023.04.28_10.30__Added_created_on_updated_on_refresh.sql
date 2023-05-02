ALTER TABLE refresh_token
ADD COLUMN created_on_utc timestamp default now(),
ADD COLUMN updated_on_utc timestamp default now()