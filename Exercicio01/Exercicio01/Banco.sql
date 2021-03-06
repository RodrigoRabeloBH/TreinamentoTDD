﻿IF NOT EXISTS (SELECT 1 FROM SYS.TABLES WHERE NAME = 'PESSOA')
              CREATE TABLE PESSOA (ID BIGINT NOT NULL, 
              NOME VARCHAR(254) NOT NULL,
              EMAIL VARCHAR(254),
              NASCIMENTO DATETIME NOT NULL,
              INICIOCONTRATO DATETIME NOT NULL,
              ATIVO CHAR(1) NOT NULL,
              SEXO CHAR(1) NOT NULL,
              SALARIO DECIMAL(12,2) NOT NULL DEFAULT 0,
              CONSTRAINT PK_PESSOA PRIMARY KEY (ID))

IF NOT EXISTS (SELECT 1 FROM SYS.TABLES WHERE NAME = 'DEPENDENTE')
              CREATE TABLE DEPENDENTE (ID BIGINT identity(1,1) NOT NULL, 
              PESSOA BIGINT NOT NULL,
              NOME VARCHAR(254) NOT NULL,
              NASCIMENTO DATETIME NOT NULL,
              SEXO CHAR(1) NOT NULL,
              CONSTRAINT PK_DEPENDENTE PRIMARY KEY (ID),
              CONSTRAINT FK_PESSOA_DEPENDENTE FOREIGN KEY (PESSOA) REFERENCES PESSOA(ID))