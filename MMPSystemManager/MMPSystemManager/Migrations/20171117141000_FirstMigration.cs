using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MMPSystemManager.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminNumber = table.Column<string>(nullable: false),
                    AdminContactEmail = table.Column<string>(nullable: true),
                    AdminContactPhone = table.Column<string>(nullable: true),
                    AdminGrade = table.Column<string>(nullable: true),
                    AdminId = table.Column<string>(nullable: true),
                    AdminIdPict = table.Column<string>(nullable: true),
                    AdminLoginTime = table.Column<DateTime>(nullable: false),
                    AdminName = table.Column<string>(nullable: true),
                    AdminOnline = table.Column<bool>(nullable: false),
                    AdminPasswd = table.Column<string>(nullable: true),
                    AdminPicTime = table.Column<DateTime>(nullable: false),
                    AdminPicUpdateTime = table.Column<DateTime>(nullable: false),
                    AdminPicture = table.Column<string>(nullable: true),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminNumber);
                });

            migrationBuilder.CreateTable(
                name: "Userinfos",
                columns: table => new
                {
                    UserNumber = table.Column<Guid>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    UserContactEmail = table.Column<string>(nullable: true),
                    UserContactPhone = table.Column<string>(nullable: true),
                    UserFacepict = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    UserPicTime = table.Column<DateTime>(nullable: false),
                    UserWechatName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Userinfos", x => x.UserNumber);
                });

            migrationBuilder.CreateTable(
                name: "Userpictures",
                columns: table => new
                {
                    UserNumber = table.Column<string>(nullable: false),
                    Remark = table.Column<string>(nullable: true),
                    UserAerialPict = table.Column<string>(nullable: true),
                    UserChoosePict = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    UserPicTime = table.Column<DateTime>(nullable: false),
                    UserPictureLocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Userpictures", x => x.UserNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Userinfos");

            migrationBuilder.DropTable(
                name: "Userpictures");
        }
    }
}
