CREATE TABLE Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Password TEXT NOT NULL
);

CREATE TABLE Addresses (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId INTEGER NOT NULL,
    Name TEXT,
    Street TEXT,
    Number INTEGER,
    Floor INTEGER,
    Department TEXT,
    FOREIGN KEY (UserId) REFERENCES Users (Id)
);

CREATE TABLE Services (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    AddressId INTEGER NOT NULL,
    Name TEXT,
    Responsible_Name TEXT,
    Type TEXT,
    Payment_Frequency TEXT,
    Annual_Payment INTEGER,
    Client_Number TEXT,
    FOREIGN KEY (AddressId) REFERENCES Addresses (Id)
);

CREATE TABLE Quotas (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ServiceId INTEGER NOT NULL,
    Number INTEGER,
    Month TEXT,
    Amount INTEGER,
    Payment_Status INTEGER,
    Payed_Date TEXT,
    Expiration_Date TEXT,
    Bill_Voucher BLOB,
    Payment_Voucher BLOB,
    FOREIGN KEY (ServiceId) REFERENCES Services (Id)
);