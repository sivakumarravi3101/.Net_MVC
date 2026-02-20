CREATE TABLE
    IF NOT EXISTS users (
        id SERIAL PRIMARY KEY,
        email VARCHAR(150) NOT NULL UNIQUE,
        password_hash VARCHAR(255) NOT NULL,
        last_logged_in TIMESTAMP,
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
    );