
CREATE TABLE usuario (
    id_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nombre_completo VARCHAR(150) NOT NULL,
    usuario VARCHAR(10) NOT NULL,
    contraseña VARCHAR(20) NOT NULL,
    id_cargo_usuario INT NOT NULL,
    FOREIGN KEY (id_cargo_usuario) REFERENCES cargo_usuario(id_cargo_usuario)
);
CREATE TABLE cargo_usuario (
    id_cargo_usuario INT AUTO_INCREMENT PRIMARY KEY,
    nombre_cargo VARCHAR(50) NOT NULL
);

