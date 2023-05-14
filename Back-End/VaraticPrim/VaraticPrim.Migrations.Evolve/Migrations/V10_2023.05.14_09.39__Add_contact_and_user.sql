INSERT INTO ${schema}.contact (first_name, last_name, mobile, phone) VALUES 
    ('Virgiliu', 'Plesca', '067208737', '067208737');

INSERT INTO ${schema}.user (email, password_hash, password_salt, contact_id, role_id, created_on_utc, updated_on_utc) VALUES
    ('plesca.virgiliu@gmail.com', 'zi4tHKkceUEUY86oPRw37XP0EqIb1Zizbx66dKYpUP0=', 'gBJnf+VAkdkO3uIsG+Ufcg==', 1, 1, '2023-05-14 10:37:06.000000', '2023-05-14 10:37:06.000000')