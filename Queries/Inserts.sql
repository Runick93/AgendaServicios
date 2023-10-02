INSERT INTO Users (Name, Password)
VALUES ('Runick', 'Passw0rd');

INSERT INTO Users (Name, Password)
VALUES ('Negast', 'Passw0rd');



INSERT INTO Addresses (UserId, Name, Street, Number, Floor, Department)
VALUES (1, 'Casa de Leandro', 'Pintos', 739, 'Planta Baja', 'None');

INSERT INTO Addresses (UserId, Name, Street, Number, Floor, Department)
VALUES (1, 'Casa de Padres', 'Lynch', 3467, 'Planta Baja', 'None');

INSERT INTO Addresses (UserId, Name, Street, Number, Floor, Department)
VALUES (2, 'Casa de Fran', 'Ayacucho', 1404, '6', 'L');



INSERT INTO Services (AddressId, Name, Responsible_Name, Type, Payment_Frequency, Annual_Payment, Client_Number)
VALUES (1, 'Edesur', 'Leandro', 'Electricidad', 'Mensual', 0, 1001);

INSERT INTO Services (AddressId, Name, Responsible_Name, Type, Payment_Frequency, Annual_Payment, Client_Number)
VALUES (1, 'Aysa', 'Leandro', 'Agua', 'Mensual',  0, 1001);

INSERT INTO Services (AddressId, Name, Responsible_Name, Type, Payment_Frequency, Annual_Payment, Client_Number)
VALUES (2, 'Edesur', 'Padres', 'Electricidad', 'Mensual',  0, 1002);

INSERT INTO Services (AddressId, Name, Responsible_Name, Type, Payment_Frequency, Annual_Payment, Client_Number)
VALUES (3, 'Metrogas', 'Fran', 'Gas', 'Mensual',  0, 1001);

INSERT INTO Services (AddressId, Name, Responsible_Name, Type, Payment_Frequency, Annual_Payment, Client_Number)
VALUES (3, 'Aysa', 'Fran', 'Agua', 'Mensual',  0, 1002);



INSERT INTO Quotas (ServiceId, Number, Month, Amount, Payment_Status)
VALUES (1, 1, 'Enero', 2000, 1);

INSERT INTO Quotas (ServiceId, Number, Month, Amount, Payment_Status)
VALUES (1, 2, 'Febrero', 2000, 1);

INSERT INTO Quotas (ServiceId, Number, Month, Amount, Payment_Status)
VALUES (2, 1, 'Enero', 2000, 1);

INSERT INTO Quotas (ServiceId, Number, Month, Amount, Payment_Status)
VALUES (3, 1, 'Enero', 2000, 1);

INSERT INTO Quotas (ServiceId, Number, Month, Amount, Payment_Status)
VALUES (4, 1, 'Enero', 2000, 1);

INSERT INTO Quotas (ServiceId, Number, Month, Amount, Payment_Status)
VALUES (4, 2, 'Febrero', 2000, 1);

INSERT INTO Quotas (ServiceId, Number, Month, Amount, Payment_Status)
VALUES (5, 1, 'Enero', 2000, 1);

INSERT INTO Quotas (ServiceId, Number, Month, Amount, Payment_Status)
VALUES (5, 2, 'Febrero', 2000, 1);

INSERT INTO Quotas (ServiceId, Number, Month, Amount, Payment_Status)
VALUES (5, 3, 'Marzo', 30000, 0);