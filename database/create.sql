DROP DATABASE `fluxus`;
CREATE DATABASE `fluxus`;
USE `fluxus`;


DROP TABLE IF EXISTS BankBranch;
CREATE TABLE BankBranch (
	`Id` INT NOT NULL AUTO_INCREMENT,
	`BranchNumber` VARCHAR(4) NOT NULL,
	`Name` VARCHAR(100) NOT NULL,
	`Address` VARCHAR(100) DEFAULT NULL,
	`Complement` VARCHAR(100) DEFAULT NULL,
	`District` VARCHAR(100) DEFAULT NULL,
	`City` VARCHAR(100) DEFAULT NULL,
	`Zip` VARCHAR(9) DEFAULT NULL,
	`State` VARCHAR(2) DEFAULT NULL,
	`ContactName` VARCHAR(20) DEFAULT NULL,
	`Phone1` VARCHAR(15) DEFAULT NULL,
	`Phone2` VARCHAR(15) DEFAULT NULL,
	`Email` VARCHAR(50) DEFAULT NULL,
	
	PRIMARY KEY (`Id`),
	UNIQUE KEY `UQ_BankBranch_BranchNumber` (`BranchNumber`)
);


DROP TABLE IF EXISTS `Service`;
CREATE TABLE `Service`(
	`Id` INT NOT NULL AUTO_INCREMENT,
	`Tag` VARCHAR(10),
	`Description` VARCHAR(100),
	`ServiceAmount` DECIMAL(10,2) NOT NULL DEFAULT 0.00,
	`MileageAllowance` DECIMAL(10,2) NOT NULL DEFAULT 0.00,
	
	PRIMARY KEY (`Id`),
	UNIQUE KEY `UQ_Service_Tag` (`Tag`)
);


DROP TABLE IF EXISTS `Profile`;
CREATE TABLE `Profile` (
	`Id` INT NOT NULL AUTO_INCREMENT,
	`Cnpj` VARCHAR(18) NOT NULL,
	`TradingName` VARCHAR(50) NOT NULL,
	`CompanyName` VARCHAR(100),
	`StateId` VARCHAR(50),
	`CityId` VARCHAR(50),
	`Address` VARCHAR(100),
	`Complement` VARCHAR(100),
	`District` VARCHAR(100),
	`City` VARCHAR(100),
	`Zip` VARCHAR(9),
	`State` VARCHAR(2),
	`EstablishmentDate` DATE,
	`Phone1` VARCHAR(15),
	`Phone2` VARCHAR(15),
	`Email` VARCHAR(50),
	`BankAccountName` VARCHAR(100),
	`BankAccountType` VARCHAR(100),
	`BankAccountBranch` VARCHAR(6),
	`BankAccountDigit` VARCHAR(3),
	`BankAccountNumber` VARCHAR(20),
	`ContractorName` VARCHAR(100),
	`ContractNotice` VARCHAR(50),
	`ContractNumber` VARCHAR(50),
	`ContractEstablished` DATE,
	`ContractStart` DATE,
	`ContractEnd` DATE,
	`Logo` MEDIUMBLOB,
	
	PRIMARY KEY (`Id`)
);


DROP TABLE IF EXISTS `Professional`;
CREATE TABLE `Professional` (
	`Id` INT NOT NULL AUTO_INCREMENT,
	`Tag` VARCHAR(3) NOT NULL,
	`Name` VARCHAR(100) NOT NULL,
	`Nameid` VARCHAR(100) DEFAULT NULL,
	`Cpf` VARCHAR(14) DEFAULT NULL,
	`Birthday` date DEFAULT NULL,
	`Profession` VARCHAR(100) DEFAULT NULL,
	`PermitNumber` VARCHAR(100) DEFAULT NULL,
	`Association` VARCHAR(100) DEFAULT NULL,
	`Phone1` VARCHAR(15) DEFAULT NULL,
	`Phone2` VARCHAR(15) DEFAULT NULL,
	`Email` VARCHAR(50) DEFAULT NULL,
	`TechnicianResponsible` BIT DEFAULT 0,
	`LegalResponsible` BIT DEFAULT 0,
	`UserActive` BIT DEFAULT 0,
	`UserName` VARCHAR(40) DEFAULT NULL,
	`UserPassword` VARCHAR(15) DEFAULT NULL,
	
	PRIMARY KEY (`Id`)
);


DROP TABLE IF EXISTS `Invoice`;
CREATE TABLE `Invoice` (
	`Id` INT NOT NULL AUTO_INCREMENT,
	`Description` VARCHAR(20),
	`IssueDate` DATE,
	`SubtotalService` DECIMAL(10,2),
	`SubtotalMileageAllowance` DECIMAL(10,2),
	`Total` DECIMAL(10,2),
	
	PRIMARY KEY (`Id`)
);


DROP TABLE IF EXISTS `ServiceOrder`;
CREATE TABLE `ServiceOrder` (
	`Id` INT NOT NULL AUTO_INCREMENT,
	`ReferenceCode` VARCHAR(33) NOT NULL,
	`Branch` VARCHAR(4),
	`OrderDate` DATE,
	`Deadline` DATE,
	`ProfessionalId` INT,
	`ServiceId` INT,
	`ServiceAmount` DECIMAL(10,2) NOT NULL,
	`MileageAllowance` DECIMAL(10,2) NOT NULL,
	`Siopi` BIT,
	`CustomerName` VARCHAR(100),
	`City` VARCHAR(100),
	`ContactName` VARCHAR(100),
	`ContactPhone` VARCHAR(15),
	`Coordinates` VARCHAR(40),
	`Status` INT,
	`PendingDate` DATE,
	`SurveyDate` DATE,
	`DoneDate` DATE,
	`Comments` TEXT,
	`Invoiced` INT DEFAULT 0,
	`InvoiceId` INT DEFAULT 0,
	
	PRIMARY KEY (`Id`),
    
	CONSTRAINT FK_Professional FOREIGN KEY (`ProfessionalId`) REFERENCES `Professional`(`Id`),
	CONSTRAINT FK_Service FOREIGN KEY (`ServiceId`) REFERENCES `Service`(`Id`)
);
