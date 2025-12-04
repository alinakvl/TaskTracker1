CREATE TABLE CallSessions (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CallerId UNIQUEIDENTIFIER NOT NULL,
    ReceiverId UNIQUEIDENTIFIER NOT NULL,
    
    BoardId UNIQUEIDENTIFIER NULL,
    TaskId UNIQUEIDENTIFIER NULL,
    
    CallType NVARCHAR(50) NOT NULL DEFAULT 'Video',
    Status NVARCHAR(50) NOT NULL DEFAULT 'Initiated',
    
    StartedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    EndedAt DATETIME2 NULL,
    Duration INT NULL,
    
    CONSTRAINT FK_CallSessions_Caller FOREIGN KEY (CallerId) REFERENCES Users(Id),
    CONSTRAINT FK_CallSessions_Receiver FOREIGN KEY (ReceiverId) REFERENCES Users(Id),
    
   
    CONSTRAINT FK_CallSessions_Board FOREIGN KEY (BoardId) 
        REFERENCES Boards(Id) 
        ON DELETE NO ACTION, 
    
  
    CONSTRAINT FK_CallSessions_Task FOREIGN KEY (TaskId) 
        REFERENCES Tasks(Id) 
        ON DELETE SET NULL,
        
    CONSTRAINT CHK_CallSessions_DifferentUsers CHECK (CallerId != ReceiverId)
);

CREATE INDEX IX_CallSessions_CallerId ON CallSessions(CallerId, StartedAt DESC);
CREATE INDEX IX_CallSessions_ReceiverId ON CallSessions(ReceiverId, StartedAt DESC);