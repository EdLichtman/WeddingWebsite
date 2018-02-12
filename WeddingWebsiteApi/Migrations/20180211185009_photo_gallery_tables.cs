using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WeddingWebsiteApi.Migrations
{
    public partial class photo_gallery_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE TABLE IF NOT EXISTS `photo_album` (
  `id` INT NOT NULL,
  `name` VARCHAR(45) NOT NULL,
  `is_approved` BIT NOT NULL DEFAULT 0,
  PRIMARY KEY (`id`));


CREATE TABLE IF NOT EXISTS `photo` (
  `photo_album_id` INT NOT NULL,
  `relative_uri` VARCHAR(100) NOT NULL,
  `is_approved` BIT NOT NULL DEFAULT 0,
  INDEX `id_idx` (`photo_album_id` ASC),
  CONSTRAINT `id`
    FOREIGN KEY (`photo_album_id`)
    REFERENCES `photo_album` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

GRANT CREATE ON weddingwebsite.photo_album TO 'admin'@'localhost';
GRANT UPDATE ON weddingwebsite.photo_album TO 'admin'@'localhost';
GRANT CREATE ON weddingwebsite.photo TO 'admin'@'localhost';
GRANT UPDATE ON weddingwebsite.photo TO 'admin'@'localhost';
");
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DROP TABLE `weddingwebsite`.`photo`;
DROP TABLE `weddingwebsite`.`photo_album`;
");
        }
    }
}
