CREATE DATABASE College1en;
GO


USE College1en;
GO

--  Programs table
CREATE TABLE Programs (
    ProgId VARCHAR(5) NOT NULL PRIMARY KEY,
    ProgName VARCHAR(50) NOT NULL
);
GO

-- Courses table
CREATE TABLE Courses (
    CId VARCHAR(7) NOT NULL PRIMARY KEY,
    CName VARCHAR(50) NOT NULL,
    ProgId VARCHAR(5) NOT NULL,
    CONSTRAINT FK_Courses_Programs FOREIGN KEY (ProgId)
        REFERENCES Programs(ProgId)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO

-- Students table
CREATE TABLE Students (
    StId VARCHAR(10) NOT NULL PRIMARY KEY,
    StName VARCHAR(50) NOT NULL,
    ProgId VARCHAR(5) NOT NULL,
    CONSTRAINT FK_Students_Programs FOREIGN KEY (ProgId)
        REFERENCES Programs(ProgId)
        ON DELETE NO ACTION
        ON UPDATE CASCADE
);
GO

-- Enrollments table
CREATE TABLE Enrollments (
    StId VARCHAR(10) NOT NULL,
    CId VARCHAR(7) NOT NULL,
    FinalGrade INT,
    PRIMARY KEY (StId, CId),
    CONSTRAINT FK_Enrollments_Students FOREIGN KEY (StId)
        REFERENCES Students(StId)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT FK_Enrollments_Courses FOREIGN KEY (CId)
        REFERENCES Courses(CId)
        ON DELETE NO ACTION
        ON UPDATE CASCADE
);
GO

INSERT INTO Programs (ProgId, ProgName)
VALUES
    ('P0001', 'Computer Science'),
    ('P0002', 'Electrical Engineering'),
    ('P0003', 'Mechanical Engineering');
GO


INSERT INTO Courses (CId, CName, ProgId)
VALUES
    ('C000001', 'Introduction to Programming', 'P0001'),
    ('C000002', 'Data Structures and Algorithms', 'P0001'),
    ('C000003', 'Circuit Analysis', 'P0002'),
    ('C000004', 'Signals and Systems', 'P0002'),
    ('C000005', 'Statics', 'P0003'),
    ('C000006', 'Dynamics', 'P0003');
GO


INSERT INTO Students (StId, StName, ProgId)
VALUES
    ('S000000001', 'John Doe', 'P0001'),
    ('S000000002', 'Jane Smith', 'P0002'),
    ('S000000003', 'Bob Johnson', 'P0003');
GO


INSERT INTO Enrollments (StId, CId, FinalGrade)
VALUES
    ('S000000001', 'C000001', 85),
    ('S000000001', 'C000002', 92),
    ('S000000002', 'C000003', 80),
    ('S000000002', 'C000004', 88),
    ('S000000003', 'C000005', 75),
    ('S000000003', 'C000006', 82);
GO