DROP TABLE IF EXISTS FrequencyType;
GO;

CREATE TABLE FrequencyType (
    Id        UNIQUEIDENTIFIER  PRIMARY KEY,
    Value     INT               NOT NULL UNIQUE,
    Name      NVARCHAR(64)      NOT NULL,
);