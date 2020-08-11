-- MySQL dump 10.13  Distrib 8.0.21, for Win64 (x86_64)
--
-- Host: localhost    Database: got
-- ------------------------------------------------------
-- Server version	8.0.21

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `archivo`
--

DROP TABLE IF EXISTS `archivo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `archivo` (
  `IDArchivo` int NOT NULL,
  `DireccionArchivo` varchar(20) DEFAULT NULL,
  `NombreArchivo` varchar(20) NOT NULL,
  `DataArchivo` text,
  `IDRepositorie` varchar(32) DEFAULT NULL,
  `IDCommit` int DEFAULT NULL,
  PRIMARY KEY (`IDArchivo`),
  KEY `FK_Archivo_Repositorie` (`IDRepositorie`),
  KEY `FK_Archivo_Commited` (`IDCommit`),
  CONSTRAINT `FK_Archivo_Commited` FOREIGN KEY (`IDCommit`) REFERENCES `commited` (`IDCommit`),
  CONSTRAINT `FK_Archivo_Repositorie` FOREIGN KEY (`IDRepositorie`) REFERENCES `repositorie` (`IDRepositorie`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `archivo`
--

LOCK TABLES `archivo` WRITE;
/*!40000 ALTER TABLE `archivo` DISABLE KEYS */;
/*!40000 ALTER TABLE `archivo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `commited`
--

DROP TABLE IF EXISTS `commited`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `commited` (
  `IDCommit` int NOT NULL,
  `MessageCommit` varchar(20) NOT NULL,
  `DateCommit` datetime DEFAULT NULL,
  PRIMARY KEY (`IDCommit`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `commited`
--

LOCK TABLES `commited` WRITE;
/*!40000 ALTER TABLE `commited` DISABLE KEYS */;
/*!40000 ALTER TABLE `commited` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `repositorie`
--

DROP TABLE IF EXISTS `repositorie`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `repositorie` (
  `IDRepositorie` varchar(32) NOT NULL,
  `NameRepositorie` varchar(20) DEFAULT NULL,
  PRIMARY KEY (`IDRepositorie`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `repositorie`
--

LOCK TABLES `repositorie` WRITE;
/*!40000 ALTER TABLE `repositorie` DISABLE KEYS */;
/*!40000 ALTER TABLE `repositorie` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-08-10 23:08:00
