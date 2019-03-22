use mysql;
CREATE USER 'usuarioConn'@'%' identified by 'Mcmddppv090281';
INSERT INTO db(host, db, user, Select_Priv, Insert_priv, Update_priv, Delete_priv) VALUES('localhost','facturaelectronica','usuarioConn','Y','Y','Y','Y');
GRANT EXECUTE ON FUNCTION facturaelectronica.DiffDays TO usuarioConn;
GRANT EXECUTE ON PROCEDURE facturaelectronica.MarcaRegistrosProcesados TO usuarioConn;
FLUSH PRIVILEGES;