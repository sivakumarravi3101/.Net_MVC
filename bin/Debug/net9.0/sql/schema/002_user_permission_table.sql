CREATE TABLE
    IF NOT EXISTS permissions (
        id SERIAL PRIMARY KEY,
        user_id INT NOT NULL,
        permission_date DATE NOT NULL,
        hours_of_permission INT NOT NULL,
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
        CONSTRAINT fk_user FOREIGN KEY (user_id) REFERENCES users (id) ON DELETE CASCADE
    );