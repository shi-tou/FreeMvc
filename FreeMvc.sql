/*
Navicat MySQL Data Transfer

Source Server         : localhost
Source Server Version : 50520
Source Host           : localhost:3306
Source Database       : FreeMvc

Target Server Type    : MYSQL
Target Server Version : 50520
File Encoding         : 65001

Date: 2016-07-25 13:44:31
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for T_Permission
-- ----------------------------
DROP TABLE IF EXISTS `T_Permission`;
CREATE TABLE `T_Permission` (
  `ID` char(40) NOT NULL,
  `Type` int(11) NOT NULL COMMENT '权限类型(1-模块 2-主窗体 3-工具栏)',
  `Name` varchar(50) NOT NULL,
  `Code` varchar(50) NOT NULL COMMENT '权限编码',
  `ParentID` char(40) DEFAULT NULL COMMENT '父ID',
  `Icon` varchar(50) DEFAULT NULL,
  `Url` varchar(50) DEFAULT NULL,
  `Sort` int(11) NOT NULL DEFAULT '999',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户权限';

-- ----------------------------
-- Records of T_Permission
-- ----------------------------
INSERT INTO `T_Permission` VALUES ('2016072420523478975657', '3', '添加/编辑', 'PermissionAdd', '2016072420524455789268', null, 'User/PermissionAdd', '1');
INSERT INTO `T_Permission` VALUES ('2016072420524455781234', '3', '添加/编辑', 'RoleAdd', '2016072420545757658787', null, 'User/RoleAdd', '1');
INSERT INTO `T_Permission` VALUES ('2016072420524455782345', '2', '用户管理', 'UserList', '2016072420524455788027', null, '/User/UserList', '1');
INSERT INTO `T_Permission` VALUES ('2016072420524455783459', '3', '删除', 'PermissionDelete', '2016072420524455789268', null, 'User/PermissionDelete', '2');
INSERT INTO `T_Permission` VALUES ('2016072420524455784190', '3', '删除', 'UserDelete', '2016072420524455782345', null, 'User/UserDelete', '2');
INSERT INTO `T_Permission` VALUES ('2016072420524455786754', '3', '添加/编辑', 'UserAdd', '2016072420524455782345', null, 'User/UserAdd', '1');
INSERT INTO `T_Permission` VALUES ('2016072420524455788027', '1', '系统管理', 'SystemManage', '0', 'home', null, '99');
INSERT INTO `T_Permission` VALUES ('2016072420524455789268', '2', '权限管理', 'PermissionList', '2016072420524455788027', null, '/User/PermissionList', '3');
INSERT INTO `T_Permission` VALUES ('2016072420545757658787', '2', '角色管理', 'RoleList', '2016072420524455788027', null, '/User/RoleList', '2');
INSERT INTO `T_Permission` VALUES ('2016072420546890754324', '3', '删除', 'RoleDelete', '2016072420545757658787', null, 'User/RoleDelete', '2');

-- ----------------------------
-- Table structure for T_Role
-- ----------------------------
DROP TABLE IF EXISTS `T_Role`;
CREATE TABLE `T_Role` (
  `ID` char(40) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Remark` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户角色';

-- ----------------------------
-- Records of T_Role
-- ----------------------------
INSERT INTO `T_Role` VALUES ('2016072420524455781149', '管理号', '无');
INSERT INTO `T_Role` VALUES ('2016072420524455787890', '超级管理员', '无555');

-- ----------------------------
-- Table structure for T_RolePermission
-- ----------------------------
DROP TABLE IF EXISTS `T_RolePermission`;
CREATE TABLE `T_RolePermission` (
  `RoleID` char(40) NOT NULL,
  `PermissionID` char(40) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='角色权限关联表';

-- ----------------------------
-- Records of T_RolePermission
-- ----------------------------
INSERT INTO `T_RolePermission` VALUES ('2016072420524455787890', '2016072420524455782340');
INSERT INTO `T_RolePermission` VALUES ('2016072420524455787890', '2016072420234435344656');
INSERT INTO `T_RolePermission` VALUES ('2016072420524455787890', '2016072420524455788027');
INSERT INTO `T_RolePermission` VALUES ('2016072420524455787890', '2016072420524455782345');
INSERT INTO `T_RolePermission` VALUES ('2016072420524455787890', '2016072420524455784190');
INSERT INTO `T_RolePermission` VALUES ('2016072420524455787890', '2016072420524455786754');
INSERT INTO `T_RolePermission` VALUES ('2016072420524455787890', '2016072420524455789268');
INSERT INTO `T_RolePermission` VALUES ('2016072420524455787890', '2016072420523478975657');
INSERT INTO `T_RolePermission` VALUES ('2016072420524455787890', '2016072420524455783459');
INSERT INTO `T_RolePermission` VALUES ('2016072420524455787890', '2016072420545757658787');
INSERT INTO `T_RolePermission` VALUES ('2016072420524455787890', '2016072420524455781234');
INSERT INTO `T_RolePermission` VALUES ('2016072420524455787890', '2016072420546890754324');

-- ----------------------------
-- Table structure for T_User
-- ----------------------------
DROP TABLE IF EXISTS `T_User`;
CREATE TABLE `T_User` (
  `ID` char(40) NOT NULL,
  `UserName` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `RoleID` char(40) DEFAULT NULL,
  `CreateBy` char(40) DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='用户';

-- ----------------------------
-- Records of T_User
-- ----------------------------
INSERT INTO `T_User` VALUES ('2016072420553465345439', 'admin', 'yHQYUyQqZ3Q=', '张三', '2016072420524455787890', '0', '2016-07-12 17:23:35');
