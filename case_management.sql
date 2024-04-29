CREATE DATABASE  IF NOT EXISTS `case_management` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `case_management`;
-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: case_management
-- ------------------------------------------------------
-- Server version	8.0.36

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `attorneys`
--

DROP TABLE IF EXISTS `attorneys`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `attorneys` (
  `attorney_id` int NOT NULL AUTO_INCREMENT,
  `attorney_lastname` varchar(255) DEFAULT NULL,
  `attorney_firstname` varchar(255) DEFAULT NULL,
  `attorney_middlename` varchar(255) DEFAULT NULL,
  `attorney_office` varchar(255) DEFAULT NULL,
  `attorney_phone` varchar(20) DEFAULT NULL,
  `attorney_email` varchar(255) DEFAULT NULL,
  `attorney_password` varchar(20) DEFAULT NULL,
  `attorney_status` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`attorney_id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `attorneys`
--

LOCK TABLES `attorneys` WRITE;
/*!40000 ALTER TABLE `attorneys` DISABLE KEYS */;
INSERT INTO `attorneys` VALUES (1,'Hugo','Angela','Advincula','Divorce','09291112345','angela@gmail.com','12345','Active'),(2,'Bernal','Alvan','Blanco','Tiwi','091234567','bernal@gmail.com','12345','Inactive'),(3,'Dela Cruz','John Lloyd','B','CAL','0956545645','lloyd@gmail.com','123','Inactive'),(4,'Advincula','Lila','Moreno','Nabua','09291116346','lila@gmail.com','1234','Active'),(5,'Hernandez','Agatha','Perez','Tiwi','092911123','agatha@gmail.com','1234','Inactive');
/*!40000 ALTER TABLE `attorneys` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `caseassignments`
--

DROP TABLE IF EXISTS `caseassignments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `caseassignments` (
  `assignment_id` int NOT NULL AUTO_INCREMENT,
  `case_id` int DEFAULT NULL,
  `attorney_id` int DEFAULT NULL,
  `prosecutor_id` int DEFAULT NULL,
  `assignment_date` date DEFAULT NULL,
  PRIMARY KEY (`assignment_id`),
  KEY `case_id` (`case_id`),
  KEY `attorney_id` (`attorney_id`),
  KEY `prosecutor_id` (`prosecutor_id`),
  CONSTRAINT `caseassignments_ibfk_1` FOREIGN KEY (`case_id`) REFERENCES `courtcases` (`case_id`),
  CONSTRAINT `caseassignments_ibfk_2` FOREIGN KEY (`attorney_id`) REFERENCES `attorneys` (`attorney_id`),
  CONSTRAINT `caseassignments_ibfk_3` FOREIGN KEY (`prosecutor_id`) REFERENCES `prosecutors` (`prosecutor_id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `caseassignments`
--

LOCK TABLES `caseassignments` WRITE;
/*!40000 ALTER TABLE `caseassignments` DISABLE KEYS */;
INSERT INTO `caseassignments` VALUES (1,1,4,2,'2024-03-13'),(2,2,4,1,'2024-04-13'),(3,7,4,2,'2024-04-13'),(7,5,1,2,'2024-04-13');
/*!40000 ALTER TABLE `caseassignments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `courtcases`
--

DROP TABLE IF EXISTS `courtcases`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `courtcases` (
  `case_id` int NOT NULL AUTO_INCREMENT,
  `case_number` varchar(50) DEFAULT NULL,
  `case_title` varchar(255) DEFAULT NULL,
  `case_type` varchar(50) DEFAULT NULL,
  `case_description` text,
  `case_status` varchar(50) DEFAULT NULL,
  `case_filing_date` date DEFAULT NULL,
  `case_court_location` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`case_id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `courtcases`
--

LOCK TABLES `courtcases` WRITE;
/*!40000 ALTER TABLE `courtcases` DISABLE KEYS */;
INSERT INTO `courtcases` VALUES (1,'2024-12345','Smith vs Riosa','Criminal','Rape','Inactive','2024-08-01','Legazpi'),(2,'2024-56789','Rodriguez vs Sariano','Criminal','Robery','Inactive','2020-01-09','Legazpi'),(3,'2024-98765','Poli vs People','Criminal','roberry','Inactive','2023-02-09','legazpi'),(5,'2024-98768','Santino vs Sariano','Criminal','Robery','Active','2020-01-02','Legazpi'),(6,'2024-98768','Santino vs Sariano','Criminal','Robery','Active','2020-01-02','Legazpi'),(7,'2023-10001','Liyosa vs Nebres','Legal','Libel','Active','2023-01-11','Naga'),(8,'2023-10002','Liyosa vs Hugo','Legal','Libel','Active','2023-01-11','Naga'),(9,'2024-4567','Smith vs Santos','Criminal','Rape','Active','2024-08-02','Legazpi'),(10,'2021-1111','Limuel VS People','Criminal','Rape','Inactive','2024-08-03','Legazpi'),(11,'2024-12345','Smith vs Lim','Criminal','Rape','Inactive','2024-08-04','Legazpi'),(12,'2023-1111','123 vs 456','Legal','Dispute','Active','2024-04-13','Legazpi');
/*!40000 ALTER TABLE `courtcases` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `defendants`
--

DROP TABLE IF EXISTS `defendants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `defendants` (
  `defendant_id` int NOT NULL AUTO_INCREMENT,
  `defendant_name` varchar(255) DEFAULT NULL,
  `defendant_address` varchar(255) DEFAULT NULL,
  `defendant_phone` varchar(20) DEFAULT NULL,
  `defendant_attorney_id` int DEFAULT NULL,
  PRIMARY KEY (`defendant_id`),
  KEY `defendant_attorney_id` (`defendant_attorney_id`),
  CONSTRAINT `defendants_ibfk_1` FOREIGN KEY (`defendant_attorney_id`) REFERENCES `attorneys` (`attorney_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `defendants`
--

LOCK TABLES `defendants` WRITE;
/*!40000 ALTER TABLE `defendants` DISABLE KEYS */;
/*!40000 ALTER TABLE `defendants` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `judges`
--

DROP TABLE IF EXISTS `judges`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `judges` (
  `judge_id` int NOT NULL AUTO_INCREMENT,
  `judge_lastname` varchar(50) NOT NULL,
  `judge_firstname` varchar(50) NOT NULL,
  `judge_office` varchar(100) DEFAULT NULL,
  `judge_phonenumber` varchar(20) DEFAULT NULL,
  `judge_email` varchar(100) NOT NULL,
  `judge_password` varchar(255) NOT NULL,
  PRIMARY KEY (`judge_id`),
  UNIQUE KEY `judge_email` (`judge_email`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `judges`
--

LOCK TABLES `judges` WRITE;
/*!40000 ALTER TABLE `judges` DISABLE KEYS */;
INSERT INTO `judges` VALUES (1,'Hugo','Angela','Nabua','091234567','hugo@gmail.com','1234');
/*!40000 ALTER TABLE `judges` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prosecutors`
--

DROP TABLE IF EXISTS `prosecutors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prosecutors` (
  `prosecutor_id` int NOT NULL AUTO_INCREMENT,
  `prosecutor_lastname` varchar(255) DEFAULT NULL,
  `prosecutor_firstname` varchar(255) DEFAULT NULL,
  `prosecutor_middlename` varchar(255) DEFAULT NULL,
  `prosecutor_office` varchar(255) DEFAULT NULL,
  `prosecutor_phone` varchar(20) DEFAULT NULL,
  `prosecutor_email` varchar(255) DEFAULT NULL,
  `prosecutor_password` varchar(20) DEFAULT NULL,
  `prosecutor_status` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`prosecutor_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prosecutors`
--

LOCK TABLES `prosecutors` WRITE;
/*!40000 ALTER TABLE `prosecutors` DISABLE KEYS */;
INSERT INTO `prosecutors` VALUES (1,'Saberon','Amir','Sebi','Legazpi','0929123345','amir@gmail.com','qwerty','Active'),(2,'Riosa','Joseph','Borlagdatan','Tabaco','091234861','riosa@gmail.com','12345','Active');
/*!40000 ALTER TABLE `prosecutors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'case_management'
--

--
-- Dumping routines for database 'case_management'
--
/*!50003 DROP FUNCTION IF EXISTS `CalculateCaseDuration` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` FUNCTION `CalculateCaseDuration`(
    p_case_id INT
) RETURNS int
    READS SQL DATA
BEGIN
    DECLARE filing_date DATE;
    DECLARE duration INT;

    SELECT case_filing_date INTO filing_date
    FROM CourtCases
    WHERE case_id = p_case_id;

    SET duration = DATEDIFF(CURDATE(), filing_date);

    RETURN duration;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `AssignAttorneyToCase` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `AssignAttorneyToCase`(
    IN p_case_id INT,
    IN p_attorney_id INT
)
BEGIN
    INSERT INTO CaseAssignments (case_id, attorney_id, assignment_date)
    VALUES (p_case_id, p_attorney_id, CURDATE());
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `CountCaseTypes` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8mb4 */ ;
/*!50003 SET character_set_results = utf8mb4 */ ;
/*!50003 SET collation_connection  = utf8mb4_0900_ai_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `CountCaseTypes`()
BEGIN
    CREATE TEMPORARY TABLE case_type_count_table (
        case_type INT,
        num_cases INT
    );

    INSERT INTO case_type_count_table
    SELECT case_type, COUNT(*) as num_cases
    FROM CourtCases
    GROUP BY case_type;

    SELECT * FROM case_type_count_table;

    DROP TEMPORARY TABLE IF EXISTS case_type_count_table;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-04-29  9:47:55
