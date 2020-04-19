using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeddingPlanner.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AllWeddings",
                columns: table => new
                {
                    WeddingId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Bride = table.Column<string>(nullable: false),
                    Groom = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllWeddings", x => x.WeddingId);
                    table.ForeignKey(
                        name: "FK_AllWeddings_AllUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AllUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllRSVPs",
                columns: table => new
                {
                    RSVPId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WeddingId = table.Column<int>(nullable: false),
                    AttendeeId = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllRSVPs", x => x.RSVPId);
                    table.ForeignKey(
                        name: "FK_AllRSVPs_AllUsers_AttendeeId",
                        column: x => x.AttendeeId,
                        principalTable: "AllUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllRSVPs_AllWeddings_WeddingId",
                        column: x => x.WeddingId,
                        principalTable: "AllWeddings",
                        principalColumn: "WeddingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllRSVPs_AttendeeId",
                table: "AllRSVPs",
                column: "AttendeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AllRSVPs_WeddingId",
                table: "AllRSVPs",
                column: "WeddingId");

            migrationBuilder.CreateIndex(
                name: "IX_AllWeddings_CreatorId",
                table: "AllWeddings",
                column: "CreatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllRSVPs");

            migrationBuilder.DropTable(
                name: "AllWeddings");

            migrationBuilder.DropTable(
                name: "AllUsers");
        }
    }
}
