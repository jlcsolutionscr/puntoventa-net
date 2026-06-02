ALTER TABLE catalogocontable DROP COLUMN IdTipoCuenta;
ALTER TABLE catalogocontable DROP CONSTRAINT catalogocontable_ibfk_2;
ALTER TABLE parametrocontable DROP CONSTRAINT parametrocontable_ibfk_1;
DROP TABLE clasecuentacontable;
DROP TABLE tipocuentacontable;
DROP TABLE tipoparametrocontable;
