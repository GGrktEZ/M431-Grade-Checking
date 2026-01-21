CREATE DATABASE  IF NOT EXISTS `grade_tracking` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `grade_tracking`;
-- MySQL dump 10.13  Distrib 8.0.43, for Win64 (x86_64)
--
-- Host: localhost    Database: grade_tracking
-- ------------------------------------------------------
-- Server version	8.0.43

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
-- Table structure for table `class_enrollments`
--

DROP TABLE IF EXISTS `class_enrollments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `class_enrollments` (
  `enrollment_id` int NOT NULL AUTO_INCREMENT,
  `class_id` int NOT NULL,
  `student_id` int NOT NULL,
  PRIMARY KEY (`enrollment_id`),
  UNIQUE KEY `uq_class_student` (`class_id`,`student_id`),
  KEY `fk_enrollments_student` (`student_id`),
  CONSTRAINT `fk_enrollments_class` FOREIGN KEY (`class_id`) REFERENCES `classes` (`class_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_enrollments_student` FOREIGN KEY (`student_id`) REFERENCES `students` (`student_id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `class_enrollments`
--

LOCK TABLES `class_enrollments` WRITE;
/*!40000 ALTER TABLE `class_enrollments` DISABLE KEYS */;
INSERT INTO `class_enrollments` VALUES (1,4,4),(2,4,5),(3,4,6),(4,5,7),(5,5,8),(6,5,9),(7,6,10),(8,6,11),(9,6,12),(10,7,13),(11,7,14),(12,7,15),(13,8,16),(14,8,17),(15,8,18);
/*!40000 ALTER TABLE `class_enrollments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `classes`
--

DROP TABLE IF EXISTS `classes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `classes` (
  `class_id` int NOT NULL AUTO_INCREMENT,
  `class_name` varchar(150) NOT NULL,
  `description` text,
  PRIMARY KEY (`class_id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `classes`
--

LOCK TABLES `classes` WRITE;
/*!40000 ALTER TABLE `classes` DISABLE KEYS */;
INSERT INTO `classes` VALUES (4,'INFAWU1','Informatikklasse 2025 A'),(5,'IM23b','Informatikklasse 2023 B'),(6,'BM24a','Berufsmaturität 2024 A'),(7,'IM24a','Informatikklasse 2024 A'),(8,'GS24a','Gesundheit & Soziales 2024 A');
/*!40000 ALTER TABLE `classes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `departments`
--

DROP TABLE IF EXISTS `departments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `departments` (
  `department_id` int NOT NULL AUTO_INCREMENT,
  `department_name` varchar(200) NOT NULL,
  PRIMARY KEY (`department_id`),
  UNIQUE KEY `department_name` (`department_name`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `departments`
--

LOCK TABLES `departments` WRITE;
/*!40000 ALTER TABLE `departments` DISABLE KEYS */;
INSERT INTO `departments` VALUES (3,'Automobil | Technik | Informatik'),(5,'BM | ABU | Sport'),(2,'Gesundheit | Soziales | Dienstleistung'),(4,'Planung | Infrastruktur | Innenausbau'),(1,'Rektorat'),(6,'Services');
/*!40000 ALTER TABLE `departments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `grade_changes`
--

DROP TABLE IF EXISTS `grade_changes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `grade_changes` (
  `change_id` int NOT NULL AUTO_INCREMENT,
  `class_id` int NOT NULL,
  `student_id` int NOT NULL,
  `module_id` int NOT NULL,
  `teacher_id` int NOT NULL,
  `prorector_id` int NOT NULL,
  `assessment_title` varchar(150) NOT NULL,
  `old_grade_value` varchar(10) NOT NULL,
  `new_grade_value` varchar(10) NOT NULL,
  `comment` text,
  PRIMARY KEY (`change_id`),
  KEY `fk_gc_class` (`class_id`),
  KEY `fk_gc_student` (`student_id`),
  KEY `fk_gc_module` (`module_id`),
  KEY `fk_gc_teacher` (`teacher_id`),
  KEY `fk_gc_prorector` (`prorector_id`),
  CONSTRAINT `fk_gc_class` FOREIGN KEY (`class_id`) REFERENCES `classes` (`class_id`) ON DELETE RESTRICT,
  CONSTRAINT `fk_gc_module` FOREIGN KEY (`module_id`) REFERENCES `modules` (`module_id`) ON DELETE RESTRICT,
  CONSTRAINT `fk_gc_prorector` FOREIGN KEY (`prorector_id`) REFERENCES `prorectors` (`prorector_id`) ON DELETE RESTRICT,
  CONSTRAINT `fk_gc_student` FOREIGN KEY (`student_id`) REFERENCES `students` (`student_id`) ON DELETE RESTRICT,
  CONSTRAINT `fk_gc_teacher` FOREIGN KEY (`teacher_id`) REFERENCES `teachers` (`teacher_id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `grade_changes`
--

LOCK TABLES `grade_changes` WRITE;
/*!40000 ALTER TABLE `grade_changes` DISABLE KEYS */;
INSERT INTO `grade_changes` VALUES (1,5,9,3,17,7,'Neue Prüf 1','4.5','5.2','Erste nachprüfung');
/*!40000 ALTER TABLE `grade_changes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `modules`
--

DROP TABLE IF EXISTS `modules`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `modules` (
  `module_id` int NOT NULL AUTO_INCREMENT,
  `module_code` varchar(50) NOT NULL,
  `module_name` varchar(150) NOT NULL,
  `description` text,
  PRIMARY KEY (`module_id`),
  UNIQUE KEY `module_code` (`module_code`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `modules`
--

LOCK TABLES `modules` WRITE;
/*!40000 ALTER TABLE `modules` DISABLE KEYS */;
INSERT INTO `modules` VALUES (1,'M117','Informatik Grundlagen','Grundlagen Programmierung und Konzepte'),(2,'M319','Datenbanken','SQL, Relationen, Normalisierung'),(3,'M431','Netzwerke','TCP/IP, Routing, Subnetting');
/*!40000 ALTER TABLE `modules` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `prorectors`
--

DROP TABLE IF EXISTS `prorectors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `prorectors` (
  `prorector_id` int NOT NULL AUTO_INCREMENT,
  `first_name` varchar(100) NOT NULL,
  `last_name` varchar(100) NOT NULL,
  `email` varchar(150) NOT NULL,
  `department_id_1` int NOT NULL,
  `department_id_2` int DEFAULT NULL,
  PRIMARY KEY (`prorector_id`),
  UNIQUE KEY `email` (`email`),
  KEY `fk_prorectors_department_1` (`department_id_1`),
  KEY `fk_prorectors_department_2` (`department_id_2`),
  CONSTRAINT `fk_prorectors_department_1` FOREIGN KEY (`department_id_1`) REFERENCES `departments` (`department_id`) ON DELETE RESTRICT,
  CONSTRAINT `fk_prorectors_department_2` FOREIGN KEY (`department_id_2`) REFERENCES `departments` (`department_id`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `prorectors`
--

LOCK TABLES `prorectors` WRITE;
/*!40000 ALTER TABLE `prorectors` DISABLE KEYS */;
INSERT INTO `prorectors` VALUES (1,'Patrick','Stalder','patrick.stalder@zg.ch',1,NULL),(2,'Regula','Tobler','regula.tobler@zg.ch',2,NULL),(3,'Werner','Odermatt','werner.odermatt@zg.ch',3,NULL),(4,'Patrick','Zeiger','patrick.zeiger@zg.ch',4,NULL),(5,'Alex','Kobel','alex.kobel@zg.ch',5,NULL),(6,'Peter','Lötscher','peter.loetscher@zg.ch',6,NULL),(7,'David','Seeliger','dseeliger@online.gibz.ch',2,3);
/*!40000 ALTER TABLE `prorectors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `students`
--

DROP TABLE IF EXISTS `students`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `students` (
  `student_id` int NOT NULL AUTO_INCREMENT,
  `first_name` varchar(100) NOT NULL,
  `last_name` varchar(100) NOT NULL,
  `email` varchar(150) NOT NULL,
  PRIMARY KEY (`student_id`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `students`
--

LOCK TABLES `students` WRITE;
/*!40000 ALTER TABLE `students` DISABLE KEYS */;
INSERT INTO `students` VALUES (4,'Yilmaz','Bozkan','yilmaz.bozkan@online.gibz.ch'),(5,'Luca','Meier','luca.meier@online.gibz.ch'),(6,'Sofia','Schmid','sofia.schmid@online.gibz.ch'),(7,'Noah','Brunner','noah.brunner@online.gibz.ch'),(8,'Mia','Koch','mia.koch@online.gibz.ch'),(9,'Elias','Fischer','elias.fischer@online.gibz.ch'),(10,'Lea','Huber','lea.huber@online.gibz.ch'),(11,'Tim','Keller','tim.keller@online.gibz.ch'),(12,'Nina','Weber','nina.weber@online.gibz.ch'),(13,'Jonas','Baumann','jonas.baumann@online.gibz.ch'),(14,'Sara','Graf','sara.graf@online.gibz.ch'),(15,'Liam','Suter','liam.suter@online.gibz.ch'),(16,'Emma','Brunner','emma.brunner@online.gibz.ch'),(17,'David','Moser','david.moser@online.gibz.ch'),(18,'Alina','Bühler','alina.buehler@online.gibz.ch');
/*!40000 ALTER TABLE `students` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `teacher_classes`
--

DROP TABLE IF EXISTS `teacher_classes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `teacher_classes` (
  `teacher_id` int NOT NULL,
  `class_id` int NOT NULL,
  `module_id` int NOT NULL,
  PRIMARY KEY (`teacher_id`,`class_id`,`module_id`),
  KEY `fk_tc_class` (`class_id`),
  KEY `fk_tc_module` (`module_id`),
  CONSTRAINT `fk_tc_class` FOREIGN KEY (`class_id`) REFERENCES `classes` (`class_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_tc_module` FOREIGN KEY (`module_id`) REFERENCES `modules` (`module_id`) ON DELETE RESTRICT,
  CONSTRAINT `fk_tc_teacher` FOREIGN KEY (`teacher_id`) REFERENCES `teachers` (`teacher_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teacher_classes`
--

LOCK TABLES `teacher_classes` WRITE;
/*!40000 ALTER TABLE `teacher_classes` DISABLE KEYS */;
INSERT INTO `teacher_classes` VALUES (14,4,3),(15,4,3),(14,5,2),(17,5,2),(8,6,1),(17,6,3),(5,7,3),(4,8,2);
/*!40000 ALTER TABLE `teacher_classes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `teacher_modules`
--

DROP TABLE IF EXISTS `teacher_modules`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `teacher_modules` (
  `teacher_id` int NOT NULL,
  `module_id` int NOT NULL,
  PRIMARY KEY (`teacher_id`,`module_id`),
  KEY `fk_tm_module` (`module_id`),
  CONSTRAINT `fk_tm_module` FOREIGN KEY (`module_id`) REFERENCES `modules` (`module_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_tm_teacher` FOREIGN KEY (`teacher_id`) REFERENCES `teachers` (`teacher_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teacher_modules`
--

LOCK TABLES `teacher_modules` WRITE;
/*!40000 ALTER TABLE `teacher_modules` DISABLE KEYS */;
INSERT INTO `teacher_modules` VALUES (14,1),(15,1);
/*!40000 ALTER TABLE `teacher_modules` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `teacher_prorectors`
--

DROP TABLE IF EXISTS `teacher_prorectors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `teacher_prorectors` (
  `teacher_id` int NOT NULL,
  `prorector_id_1` int NOT NULL,
  `prorector_id_2` int DEFAULT NULL,
  PRIMARY KEY (`teacher_id`),
  KEY `fk_tp_prorector_1` (`prorector_id_1`),
  KEY `fk_tp_prorector_2` (`prorector_id_2`),
  CONSTRAINT `fk_tp_prorector_1` FOREIGN KEY (`prorector_id_1`) REFERENCES `prorectors` (`prorector_id`) ON DELETE RESTRICT,
  CONSTRAINT `fk_tp_prorector_2` FOREIGN KEY (`prorector_id_2`) REFERENCES `prorectors` (`prorector_id`) ON DELETE SET NULL,
  CONSTRAINT `fk_tp_teacher` FOREIGN KEY (`teacher_id`) REFERENCES `teachers` (`teacher_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teacher_prorectors`
--

LOCK TABLES `teacher_prorectors` WRITE;
/*!40000 ALTER TABLE `teacher_prorectors` DISABLE KEYS */;
INSERT INTO `teacher_prorectors` VALUES (4,2,NULL),(5,3,NULL),(6,5,NULL),(7,4,NULL),(8,1,5),(9,2,6),(10,3,4),(11,1,6),(12,3,NULL),(13,2,NULL),(14,3,NULL),(15,3,NULL),(17,2,7);
/*!40000 ALTER TABLE `teacher_prorectors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `teachers`
--

DROP TABLE IF EXISTS `teachers`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `teachers` (
  `teacher_id` int NOT NULL AUTO_INCREMENT,
  `first_name` varchar(100) NOT NULL,
  `last_name` varchar(100) NOT NULL,
  `email` varchar(150) NOT NULL,
  `department_id_1` int DEFAULT NULL,
  `department_id_2` int DEFAULT NULL,
  `password_hash` varchar(500) DEFAULT NULL,
  `email_confirmed` tinyint(1) NOT NULL DEFAULT '0',
  `email_confirmation_token_hash` varchar(500) DEFAULT NULL,
  `email_confirmation_expires_at` datetime DEFAULT NULL,
  `login_token_hash` varchar(255) DEFAULT NULL,
  `login_token_expires_at` datetime DEFAULT NULL,
  `registration_requested_at` datetime DEFAULT NULL,
  `email_confirmed_at` datetime DEFAULT NULL,
  PRIMARY KEY (`teacher_id`),
  UNIQUE KEY `email` (`email`),
  KEY `fk_teachers_department_1` (`department_id_1`),
  KEY `fk_teachers_department_2` (`department_id_2`),
  KEY `idx_teachers_email_confirmed` (`email`,`email_confirmed`),
  KEY `idx_teachers_confirm_expires` (`email_confirmation_expires_at`),
  CONSTRAINT `fk_teachers_department_1` FOREIGN KEY (`department_id_1`) REFERENCES `departments` (`department_id`) ON DELETE SET NULL,
  CONSTRAINT `fk_teachers_department_2` FOREIGN KEY (`department_id_2`) REFERENCES `departments` (`department_id`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `teachers`
--

LOCK TABLES `teachers` WRITE;
/*!40000 ALTER TABLE `teachers` DISABLE KEYS */;
INSERT INTO `teachers` VALUES (4,'Julia','Abächerli','julia.abaecherli@gibz.ch',2,NULL,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL),(5,'Markus','Attinger','markus.attinger@gibz.ch',3,NULL,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL),(6,'Sonja','Appert','sonja.appert@gibz.ch',5,NULL,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL),(7,'Myriam','Arnelas','myriam.arnelas@gibz.ch',4,NULL,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL),(8,'Tanja','Furrer','tanja.furrer@gibz.ch',1,5,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL),(9,'Paul','Furrer','paul.furrer@gibz.ch',2,6,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL),(10,'Estefania','Frei-Otero','estefania.frei-otero@gibz.ch',3,4,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL),(11,'Janine','Ezer','janine.ezer@gibz.ch',1,6,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL),(12,'Kevin','Gehrig','kevin.gehrig@gibz.ch',3,NULL,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL),(13,'Nora','Frey','nora.frey@gibz.ch',2,NULL,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL),(14,'Christian','Lindauer','christian.lindauer@gibz.ch',3,NULL,NULL,0,NULL,NULL,NULL,NULL,NULL,NULL),(15,'Yilmaz','Bozkan','yilmaz.bozkan@gmail.com',3,NULL,'Mj1dhYb2W8hAKEeqshrbZzw3neD1FHAtvwGhYIS2LTOff9EEIu4XQm4o9R0niwlX',1,NULL,NULL,'c33ac09d09a33085400ac7bc196a97b1a97f6f07e8132d46436733fdd2a61672','2026-01-21 21:11:35','2026-01-20 13:18:19','2026-01-20 13:18:45'),(16,'Tom','Tom','recipient.project@gmail.com',3,NULL,'nQRikZzernrHfi279SQ3lu/CTf4bmMY9dz6kA46kbID3H3E52qOTN/F1SD9m4r2b',1,NULL,NULL,NULL,NULL,'2026-01-20 13:24:15','2026-01-20 13:24:23'),(17,'David','Seeliger','dseeliger@online.gibz.ch',2,3,'OQ8d9+B8lZYT/2aVh5QYB+BVZ10MADX8LTsofs7Owx0mvksdex8eyGXFpLV7Xv1G',1,NULL,NULL,NULL,NULL,'2026-01-21 21:12:44','2026-01-21 21:27:37');
/*!40000 ALTER TABLE `teachers` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'grade_tracking'
--

--
-- Dumping routines for database 'grade_tracking'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-01-21 23:30:49
