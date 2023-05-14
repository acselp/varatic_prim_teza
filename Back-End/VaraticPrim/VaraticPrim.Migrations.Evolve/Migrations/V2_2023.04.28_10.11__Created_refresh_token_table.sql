CREATE TABLE ${schema}.RefreshToken(
    id SERIAL PRIMARY KEY,
    email VARCHAR(255),
    refresh_token VARCHAR(255)
)