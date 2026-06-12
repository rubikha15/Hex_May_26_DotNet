IF DB_ID('FleetMaintenanceDb') IS NULL
CREATE DATABASE FleetMaintenanceDb;
GO
USE FleetMaintenanceDb;
GO

DROP TABLE IF EXISTS MaintenanceRecords;
DROP TABLE IF EXISTS Drivers;
DROP TABLE IF EXISTS Vehicles;
GO

CREATE TABLE Vehicles
(
    VehicleId INT IDENTITY(1,1) PRIMARY KEY,
    VehicleNumber VARCHAR(20) NOT NULL,
    VehicleType VARCHAR(50) NOT NULL,
    Brand VARCHAR(50) NOT NULL,
    Model VARCHAR(50) NOT NULL,
    PurchaseYear INT NOT NULL,
    IsActive BIT NOT NULL
);

CREATE TABLE Drivers
(
    DriverId INT IDENTITY(1,1) PRIMARY KEY,
    DriverName VARCHAR(100) NOT NULL,
    LicenseNumber VARCHAR(50) NOT NULL,
    PhoneNumber VARCHAR(15) NOT NULL,
    City VARCHAR(50) NOT NULL,
    IsAvailable BIT NOT NULL
);

CREATE TABLE MaintenanceRecords
(
    MaintenanceId INT IDENTITY(1,1) PRIMARY KEY,
    VehicleId INT NOT NULL,
    DriverId INT NOT NULL,
    ServiceDate DATE NOT NULL,
    ServiceType VARCHAR(100) NOT NULL,
    ServiceCost DECIMAL(18,2) NOT NULL,
    ServiceStatus VARCHAR(30) NOT NULL,
    Remarks VARCHAR(250) NULL,
    CreatedDate DATETIME NOT NULL,
    CONSTRAINT FK_MaintenanceRecords_Vehicles FOREIGN KEY (VehicleId) REFERENCES Vehicles(VehicleId),
    CONSTRAINT FK_MaintenanceRecords_Drivers FOREIGN KEY (DriverId) REFERENCES Drivers(DriverId)
);
GO

INSERT INTO Vehicles (VehicleNumber, VehicleType, Brand, Model, PurchaseYear, IsActive) VALUES
('TN38AB1234','Truck','Tata','Ace',2021,1),
('TN39CD5678','Van','Mahindra','Bolero',2020,1),
('TN40EF9012','Mini Truck','Ashok Leyland','Dost',2022,1),
('TN41GH3456','Truck','Eicher','Pro 2049',2019,1),
('TN42IJ7890','Van','Force','Traveller',2023,1),
('TN43KL1122','Truck','BharatBenz','1217C',2021,1),
('TN44MN3344','Pickup','Isuzu','D-Max',2022,1),
('TN45OP5566','Van','Maruti','Eeco Cargo',2020,1),
('TN46QR7788','Truck','Tata','Ultra',2018,0),
('TN47ST9900','Mini Truck','Mahindra','Jeeto',2024,1);

INSERT INTO Drivers (DriverName, LicenseNumber, PhoneNumber, City, IsAvailable) VALUES
('Ramesh Kumar','DL2026TN1001','9876543210','Coimbatore',1),
('Suresh Babu','DL2026TN1002','9876543211','Chennai',1),
('Karthik Raj','DL2026TN1003','9876543212','Madurai',1),
('Prakash M','DL2026TN1004','9876543213','Trichy',0),
('Vignesh R','DL2026TN1005','9876543214','Salem',1),
('Arun Kumar','DL2026TN1006','9876543215','Erode',1),
('Manikandan S','DL2026TN1007','9876543216','Tirunelveli',0),
('Rajesh P','DL2026TN1008','9876543217','Thanjavur',1),
('Muthu Kumar','DL2026TN1009','9876543218','Nagapattinam',1),
('Senthil N','DL2026TN1010','9876543219','Vellore',1);

INSERT INTO MaintenanceRecords (VehicleId, DriverId, ServiceDate, ServiceType, ServiceCost, ServiceStatus, Remarks, CreatedDate) VALUES
(1,1,'2026-06-01','Oil Change',2500,'Completed','Regular oil replacement','2026-05-30T10:00:00'),
(2,2,'2026-06-02','Brake Inspection',1800,'Completed','Brake pad inspection','2026-05-30T10:05:00'),
(3,3,'2026-06-03','Engine Repair',12500,'InProgress','Engine noise issue','2026-05-31T09:00:00'),
(4,4,'2026-06-04','Tyre Replacement',9000,'Scheduled','Replace front tyres','2026-05-31T09:15:00'),
(5,5,'2026-06-05','Battery Check',1200,'Completed','Battery voltage low','2026-06-01T08:30:00'),
(6,6,'2026-06-06','General Service',5000,'Scheduled','Routine checkup','2026-06-01T08:45:00'),
(7,7,'2026-06-07','Oil Change',2600,'Cancelled','Vehicle unavailable','2026-06-02T11:00:00'),
(8,8,'2026-06-08','Brake Inspection',2200,'Completed','Brake fluid refill','2026-06-02T11:30:00'),
(9,9,'2026-06-09','Engine Repair',15000,'InProgress','Overheating issue','2026-06-03T12:00:00'),
(10,10,'2026-06-10','Tyre Replacement',8500,'Completed','Rear tyres replaced','2026-06-03T12:20:00'),
(1,2,'2026-06-11','Battery Check',1300,'Scheduled','Battery terminal cleaning','2026-06-04T09:10:00'),
(2,3,'2026-06-12','General Service',4800,'Completed','Full inspection','2026-06-04T09:20:00'),
(3,4,'2026-06-13','Oil Change',2400,'Completed','Oil and filter changed','2026-06-05T10:15:00'),
(4,5,'2026-06-14','Brake Inspection',2100,'Scheduled','Brake sound complaint','2026-06-05T10:30:00'),
(5,6,'2026-06-15','Engine Repair',11000,'InProgress','Engine tuning','2026-06-06T13:00:00'),
(6,7,'2026-06-16','Tyre Replacement',9200,'Completed','All tyres balanced','2026-06-06T13:10:00'),
(7,8,'2026-06-17','Battery Check',1500,'Completed','Battery replaced','2026-06-07T14:00:00'),
(8,9,'2026-06-18','General Service',5200,'Cancelled','Driver unavailable','2026-06-07T14:20:00'),
(9,10,'2026-06-19','Oil Change',2700,'Scheduled','Regular maintenance','2026-06-08T15:00:00'),
(10,1,'2026-06-20','Brake Inspection',2000,'Completed','Brake shoe checked','2026-06-08T15:30:00'),
(1,3,'2026-06-21','Engine Repair',13500,'InProgress','Engine belt issue','2026-06-09T09:00:00'),
(2,4,'2026-06-22','Tyre Replacement',8700,'Scheduled','One tyre damaged','2026-06-09T09:30:00'),
(3,5,'2026-06-23','Battery Check',1400,'Completed','Battery water filled','2026-06-10T10:00:00'),
(4,6,'2026-06-24','General Service',5500,'Completed','Routine general service','2026-06-10T10:45:00'),
(5,7,'2026-06-25','Oil Change',2300,'Cancelled','Service postponed','2026-06-11T11:00:00'),
(6,8,'2026-06-26','Brake Inspection',1900,'Completed','Brake cable adjusted','2026-06-11T11:20:00'),
(7,9,'2026-06-27','Engine Repair',16000,'Scheduled','Engine vibration issue','2026-06-12T08:50:00'),
(8,10,'2026-06-28','Tyre Replacement',9300,'Completed','Two tyres replaced','2026-06-12T09:10:00'),
(9,1,'2026-06-29','Battery Check',1250,'InProgress','Battery charging issue','2026-06-12T09:30:00'),
(10,2,'2026-06-30','General Service',5100,'Completed','Monthly service completed','2026-06-12T09:45:00');
GO
