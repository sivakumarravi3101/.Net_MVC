CREATE TABLE
    IF NOT EXISTS users (
        id SERIAL PRIMARY KEY,
        email VARCHAR(150) NOT NULL UNIQUE,
        email VARCHAR(250),
        password_hash VARCHAR(255) NOT NULL,
        last_logged_in TIMESTAMPTZ,
        created_at TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP,
        is_active BOOLEAN DEFAULT TRUE,
        role VARCHAR(50) DEFAULT 'User'
    );