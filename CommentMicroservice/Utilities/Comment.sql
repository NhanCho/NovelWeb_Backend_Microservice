-- Tạo database nếu chưa tồn tại
CREATE DATABASE IF NOT EXISTS comment_db;
USE comment_db;

-- Xóa bảng `comments` nếu nó đã tồn tại
DROP TABLE IF EXISTS `comments`;

-- Tạo bảng `comments`
CREATE TABLE `comments` (
  `CommentID` int NOT NULL AUTO_INCREMENT,
  `NovelID` int NOT NULL,
  `UserID` int NOT NULL,
  `Content` longtext NOT NULL,
  `CreatedDate` datetime NOT NULL,
  PRIMARY KEY (`CommentID`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- Chèn dữ liệu mẫu vào bảng `comments`
INSERT INTO `comments` (`CommentID`, `NovelID`, `UserID`, `Content`, `CreatedDate`) 
VALUES 
  (2, 0, 0, 'Testtt', '2024-12-04 23:53:44'),
  (3, 0, 0, 'taaaa', '2024-12-04 23:54:25'),
  (4, 0, 0, 'Testdot1', '2024-12-05 00:14:53'),
  (5, 0, 0, 'TestLanCuoi', '2024-12-05 00:16:45');

-- Kiểm tra dữ liệu trong bảng `comments`
SELECT * FROM `comments`;
