CREATE TABLE Archivo (
    IDArchivo varchar(32) NOT NULL,
    DireccionArchivo varchar(20) NOT NULL,
    DataArchivo varchar(40),
    NombreArchivo varchar(40),
    CONSTRAINT PK_Archivos PRIMARY KEY (IDArchivo)
);

CREATE TABLE Commited (
    IDCommit int NOT NULL,
    MessageCommit varchar(20) NOT NULL,
    DateCommit date,
    CONSTRAINT PK_Commited PRIMARY KEY (IDCommit)
);

CREATE TABLE Repositorie (
    IDArchivo varchar(32) NOT NULL,
    IDCommit int NOT NULL,
    NameRepo varchar(32),
    CONSTRAINT PK_Repositorie PRIMARY KEY (IDArchivo,IDCommit),
    CONSTRAINT FK_Repositorie_Archivo FOREIGN KEY (IDArchivo) REFERENCES Archivo(IDArchivo),
    CONSTRAINT FK_Repositorie_Commited FOREIGN KEY (IDCommit) REFERENCES Commited(IDCommit)
);
