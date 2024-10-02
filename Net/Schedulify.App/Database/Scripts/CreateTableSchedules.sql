DROP TABLE Schedules;
GO;

CREATE TABLE Schedules (
    Id          UNIQUEIDENTIFIER  PRIMARY KEY,
    CalendarId  UNIQUEIDENTIFIER  NOT NULL,
    CategoryId  UNIQUEIDENTIFIER  NOT NULL,
    TimeStart   DATETIMEOFFSET    NOT NULL,
    TimeEnd     DATETIMEOFFSET    NOT NULL,
    Frequency   INT               NOT NULL,
    Title       NVARCHAR(64)      NOT NULL,
    Description NVARCHAR(MAX)     NOT NULL,
    Link        NVARCHAR(MAX),
    CreatedAt   DATETIMEOFFSET    NOT NULL,
    UpdatedAt   DATETIMEOFFSET    NOT NULL,
    Author      UNIQUEIDENTIFIER  NOT NULL,
        
    CONSTRAINT FK_SchedulesAuthor FOREIGN KEY (Author) REFERENCES Users(Id),
    CONSTRAINT FK_SchedulesCalendar FOREIGN KEY (CalendarId) REFERENCES Calendars(Id),
    CONSTRAINT FK_SchedulesCategory FOREIGN KEY (CategoryId) REFERENCES Categories(Id),
    CONSTRAINT FK_SchedulesFrequencyType FOREIGN KEY (Frequency) REFERENCES FrequencyType(Value)
);