DROP DATABASE `fluxus`;
CREATE DATABASE `fluxus`;
USE `fluxus`;


DROP TABLE IF EXISTS bank_branch;
CREATE TABLE bank_branch (
	`id` INT NOT NULL AUTO_INCREMENT,
	`branch_number` VARCHAR(4) NOT NULL,
	`name` VARCHAR(100) NOT NULL,
	`address` VARCHAR(100) DEFAULT NULL,
	`complement` VARCHAR(100) DEFAULT NULL,
	`district` VARCHAR(100) DEFAULT NULL,
	`city` VARCHAR(100) DEFAULT NULL,
	`zip` VARCHAR(9) DEFAULT NULL,
	`state` VARCHAR(2) DEFAULT NULL,
	`contact_name` VARCHAR(20) DEFAULT NULL,
	`phone1` VARCHAR(15) DEFAULT NULL,
	`phone2` VARCHAR(15) DEFAULT NULL,
	`email` VARCHAR(50) DEFAULT NULL,
	
	PRIMARY KEY (`id`),
	UNIQUE KEY `UQ_bank_branch_branch_number` (`branch_number`)
);


DROP TABLE IF EXISTS `service`;
CREATE TABLE `service`(
	`id` INT NOT NULL AUTO_INCREMENT,
	`tag` VARCHAR(10),
	`description` VARCHAR(100),
	`service_amount` DECIMAL(10,2) NOT NULL DEFAULT 0.00,
	`mileage_allowance` DECIMAL(10,2) NOT NULL DEFAULT 0.00,
	
	PRIMARY KEY (`id`),
	UNIQUE KEY `UQ_services_tag` (`tag`)
);


DROP TABLE IF EXISTS `profile`;
CREATE TABLE `profile` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`cnpj` VARCHAR(18) NOT NULL,
	`trading_name` VARCHAR(50) NOT NULL,
	`company_name` VARCHAR(100),
	`state_id` VARCHAR(50),
	`city_id` VARCHAR(50),
	`address` VARCHAR(100),
	`complement` VARCHAR(100),
	`district` VARCHAR(100),
	`city` VARCHAR(100),
	`zip` VARCHAR(9),
	`state` VARCHAR(2),
	`establishment_date` DATE,
	`phone1` VARCHAR(15),
	`phone2` VARCHAR(15),
	`email` VARCHAR(50),
	`bank_account_name` VARCHAR(100),
	`bank_account_type` VARCHAR(100),
	`bank_account_branch` VARCHAR(6),
	`bank_account_digit` VARCHAR(3),
	`bank_account_number` VARCHAR(20),
	`contractor_name` VARCHAR(100),
	`contract_notice` VARCHAR(50),
	`contract_number` VARCHAR(50),
	`contract_established` DATE,
	`contract_start` DATE,
	`contract_end` DATE,
	`logo` MEDIUMBLOB,
	
	PRIMARY KEY (`id`)
);


DROP TABLE IF EXISTS `professional`;
CREATE TABLE `professional` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`tag` VARCHAR(3) NOT NULL,
	`name` VARCHAR(100) NOT NULL,
	`nameid` VARCHAR(100) DEFAULT NULL,
	`cpf` VARCHAR(14) DEFAULT NULL,
	`birthday` date DEFAULT NULL,
	`profession` VARCHAR(100) DEFAULT NULL,
	`permit_number` VARCHAR(100) DEFAULT NULL,
	`association` VARCHAR(100) DEFAULT NULL,
	`phone1` VARCHAR(15) DEFAULT NULL,
	`phone2` VARCHAR(15) DEFAULT NULL,
	`email` VARCHAR(50) DEFAULT NULL,
	`technician_responsible` BIT DEFAULT 0,
	`legal_responsible` BIT DEFAULT 0,
	`user_active` BIT DEFAULT 0,
	`user_name` VARCHAR(40) DEFAULT NULL,
	`user_password` VARCHAR(15) DEFAULT NULL,
	
	PRIMARY KEY (`id`)
);


DROP TABLE IF EXISTS `invoice`;
CREATE TABLE `invoice` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`description` VARCHAR(20),
	`issue_date` DATE,
	`subtotal_service` DECIMAL(10,2),
	`subtotal_mileage_allowance` DECIMAL(10,2),
	`total` DECIMAL(10,2),
	
	PRIMARY KEY (`id`)
);


DROP TABLE IF EXISTS `service_order`;
CREATE TABLE `service_order` (
	`id` INT NOT NULL AUTO_INCREMENT,
	`reference_code` VARCHAR(33) NOT NULL,
	`branch` VARCHAR(4),
	`title` VARCHAR(150),
	`order_date` DATE,
	`deadline` DATE,
	`professional_id` INT,
	`service_id` INT,
	`service_amount` DECIMAL(10,2) NOT NULL,
	`mileage_allowance` DECIMAL(10,2) NOT NULL,
	`siopi` BIT,
	`customer_name` VARCHAR(100),
	`city` VARCHAR(100),
	`contact_name` VARCHAR(100),
	`contact_phone` VARCHAR(15),
	`coordinates` VARCHAR(40),
	`status` VARCHAR(10),
	`pending_date` DATE,
	`survey_date` DATE,
	`done_date` DATE,
	`comments` TEXT,
	`invoice_id` INT DEFAULT 0,
	
	PRIMARY KEY (`id`),
    
	CONSTRAINT FK_Professional FOREIGN KEY (professional_id) REFERENCES professional(id),
	CONSTRAINT FK_Service FOREIGN KEY (service_id) REFERENCES service(id)
);