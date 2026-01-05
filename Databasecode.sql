CREATE DATABASE  IF NOT EXISTS `grade_tracking` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `grade_tracking`;

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

-- ----------------------------
-- Table structure
-- ----------------------------

DROP TABLE IF EXISTS `class_enrollments`;
DROP TABLE IF EXISTS `grade_changes`;
DROP TABLE IF EXISTS `teacher_prorectors`;
DROP TABLE IF EXISTS `classes`;
DROP TABLE IF EXISTS `students`;
DROP TABLE IF EXISTS `teachers`;
DROP TABLE IF EXISTS `prorectors`;
DROP TABLE IF EXISTS `modules`;
DROP TABLE IF EXISTS `departments`;

-- departments
CREATE TABLE `departments` (
  `department_id` int NOT NULL AUTO_INCREMENT,
  `department_name` varchar(200) NOT NULL,
  PRIMARY KEY (`department_id`),
  UNIQUE KEY `department_name` (`department_name`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- modules
CREATE TABLE `modules` (
  `module_id` int NOT NULL AUTO_INCREMENT,
  `module_code` varchar(50) NOT NULL,
  `module_name` varchar(150) NOT NULL,
  `description` text,
  PRIMARY KEY (`module_id`),
  UNIQUE KEY `module_code` (`module_code`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- prorectors
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
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- teachers (NOTE: includes password_hash)
CREATE TABLE `teachers` (
  `teacher_id` int NOT NULL AUTO_INCREMENT,
  `first_name` varchar(100) NOT NULL,
  `last_name` varchar(100) NOT NULL,
  `email` varchar(150) NOT NULL,
  `department_id_1` int DEFAULT NULL,
  `department_id_2` int DEFAULT NULL,
  `password_hash` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`teacher_id`),
  UNIQUE KEY `email` (`email`),
  KEY `fk_teachers_department_1` (`department_id_1`),
  KEY `fk_teachers_department_2` (`department_id_2`),
  CONSTRAINT `fk_teachers_department_1` FOREIGN KEY (`department_id_1`) REFERENCES `departments` (`department_id`) ON DELETE SET NULL,
  CONSTRAINT `fk_teachers_department_2` FOREIGN KEY (`department_id_2`) REFERENCES `departments` (`department_id`) ON DELETE SET NULL
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- students
CREATE TABLE `students` (
  `student_id` int NOT NULL AUTO_INCREMENT,
  `first_name` varchar(100) NOT NULL,
  `last_name` varchar(100) NOT NULL,
  `email` varchar(150) NOT NULL,
  PRIMARY KEY (`student_id`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- classes
CREATE TABLE `classes` (
  `class_id` int NOT NULL AUTO_INCREMENT,
  `class_name` varchar(150) NOT NULL,
  `description` text,
  `teacher_id` int NOT NULL,
  `module_id` int NOT NULL,
  PRIMARY KEY (`class_id`),
  KEY `teacher_id` (`teacher_id`),
  KEY `fk_classes_modules` (`module_id`),
  CONSTRAINT `classes_ibfk_1` FOREIGN KEY (`teacher_id`) REFERENCES `teachers` (`teacher_id`) ON DELETE CASCADE,
  CONSTRAINT `fk_classes_modules` FOREIGN KEY (`module_id`) REFERENCES `modules` (`module_id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- teacher_prorectors
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

-- grade_changes
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- class_enrollments
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

-- ----------------------------
-- Data (NO LOCK TABLES)
-- ----------------------------

INSERT INTO `departments` VALUES
  (3,'Automobil | Technik | Informatik'),
  (5,'BM | ABU | Sport'),
  (2,'Gesundheit | Soziales | Dienstleistung'),
  (4,'Planung | Infrastruktur | Innenausbau'),
  (1,'Rektorat'),
  (6,'Services');

INSERT INTO `modules` VALUES
  (1,'M117','Informatik Grundlagen','Grundlagen Programmierung und Konzepte'),
  (2,'M319','Datenbanken','SQL, Relationen, Normalisierung'),
  (3,'M431','Netzwerke','TCP/IP, Routing, Subnetting');

INSERT INTO `prorectors` VALUES
  (1,'Patrick','Stalder','patrick.stalder@zg.ch',1,NULL),
  (2,'Regula','Tobler','regula.tobler@zg.ch',2,NULL),
  (3,'Werner','Odermatt','werner.odermatt@zg.ch',3,NULL),
  (4,'Patrick','Zeiger','patrick.zeiger@zg.ch',4,NULL),
  (5,'Alex','Kobel','alex.kobel@zg.ch',5,NULL),
  (6,'Peter','Lötscher','peter.loetscher@zg.ch',6,NULL);

-- ✅ UPDATED: explicitly includes password_hash as NULL so column count matches
INSERT INTO `teachers`
  (`teacher_id`,`first_name`,`last_name`,`email`,`department_id_1`,`department_id_2`,`password_hash`)
VALUES
  (4,'Julia','Abächerli','julia.abaecherli@gibz.ch',2,NULL,NULL),
  (5,'Markus','Attinger','markus.attinger@gibz.ch',3,NULL,NULL),
  (6,'Sonja','Appert','sonja.appert@gibz.ch',5,NULL,NULL),
  (7,'Myriam','Arnelas','myriam.arnelas@gibz.ch',4,NULL,NULL),
  (8,'Tanja','Furrer','tanja.furrer@gibz.ch',1,5,NULL),
  (9,'Paul','Furrer','paul.furrer@gibz.ch',2,6,NULL),
  (10,'Estefania','Frei-Otero','estefania.frei-otero@gibz.ch',3,4,NULL),
  (11,'Janine','Ezer','janine.ezer@gibz.ch',1,6,NULL),
  (12,'Kevin','Gehrig','kevin.gehrig@gibz.ch',3,NULL,NULL),
  (13,'Nora','Frey','nora.frey@gibz.ch',2,NULL,NULL),
  (14,'Christian','Lindauer','christian.lindauer@gibz.ch',3,NULL,NULL);

INSERT INTO `students` VALUES
  (4,'Yilmaz','Bozkan','yilmaz.bozkan@online.gibz.ch'),
  (5,'Luca','Meier','luca.meier@online.gibz.ch'),
  (6,'Sofia','Schmid','sofia.schmid@online.gibz.ch'),
  (7,'Noah','Brunner','noah.brunner@online.gibz.ch'),
  (8,'Mia','Koch','mia.koch@online.gibz.ch'),
  (9,'Elias','Fischer','elias.fischer@online.gibz.ch'),
  (10,'Lea','Huber','lea.huber@online.gibz.ch'),
  (11,'Tim','Keller','tim.keller@online.gibz.ch'),
  (12,'Nina','Weber','nina.weber@online.gibz.ch'),
  (13,'Jonas','Baumann','jonas.baumann@online.gibz.ch'),
  (14,'Sara','Graf','sara.graf@online.gibz.ch'),
  (15,'Liam','Suter','liam.suter@online.gibz.ch'),
  (16,'Emma','Brunner','emma.brunner@online.gibz.ch'),
  (17,'David','Moser','david.moser@online.gibz.ch'),
  (18,'Alina','Bühler','alina.buehler@online.gibz.ch');

INSERT INTO `classes` VALUES
  (4,'INFAWU1','Informatikklasse 2025 A',14,3),
  (5,'IM23b','Informatikklasse 2023 B',14,2),
  (6,'BM24a','Berufsmaturität 2024 A',8,1),
  (7,'IM24a','Informatikklasse 2024 A',5,3),
  (8,'GS24a','Gesundheit & Soziales 2024 A',4,2);

INSERT INTO `teacher_prorectors` VALUES
  (4,2,NULL),
  (5,3,NULL),
  (6,5,NULL),
  (7,4,NULL),
  (8,1,5),
  (9,2,6),
  (10,3,4),
  (11,1,6),
  (12,3,NULL),
  (13,2,NULL),
  (14,3,NULL);

INSERT INTO `class_enrollments` VALUES
  (1,4,4),(2,4,5),(3,4,6),
  (4,5,7),(5,5,8),(6,5,9),
  (7,6,10),(8,6,11),(9,6,12),
  (10,7,13),(11,7,14),(12,7,15),
  (13,8,16),(14,8,17),(15,8,18);

-- (grade_changes has no rows)

 /*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;
 /*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
 /*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
 /*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
 /*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
 /*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
 /*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
 /*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
