using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class initialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meta",
                columns: table => new
                {
                    metaId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    resourceType = table.Column<int>(nullable: false),
                    created = table.Column<DateTime>(nullable: false),
                    lastModified = table.Column<DateTime>(nullable: false),
                    location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meta", x => x.metaId);
                });

            migrationBuilder.CreateTable(
                name: "Names",
                columns: table => new
                {
                    NameId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    familyName = table.Column<string>(nullable: true),
                    givenName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Names", x => x.NameId);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    groupType = table.Column<int>(nullable: false),
                    externalId = table.Column<string>(nullable: true),
                    displayName = table.Column<string>(nullable: true),
                    metaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.id);
                    table.ForeignKey(
                        name: "FK_Groups_Meta_metaId",
                        column: x => x.metaId,
                        principalTable: "Meta",
                        principalColumn: "metaId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    externalId = table.Column<string>(nullable: true),
                    userName = table.Column<string>(nullable: true),
                    NameId = table.Column<int>(nullable: true),
                    active = table.Column<short>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Names_NameId",
                        column: x => x.NameId,
                        principalTable: "Names",
                        principalColumn: "NameId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Emails",
                columns: table => new
                {
                    EmailId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    value = table.Column<string>(nullable: true),
                    type = table.Column<string>(nullable: true),
                    primary = table.Column<short>(type: "bit", nullable: false),
                    Userid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emails", x => x.EmailId);
                    table.ForeignKey(
                        name: "FK_Emails_Users_Userid",
                        column: x => x.Userid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emails_Userid",
                table: "Emails",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_metaId",
                table: "Groups",
                column: "metaId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_NameId",
                table: "Users",
                column: "NameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emails");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Meta");

            migrationBuilder.DropTable(
                name: "Names");
        }
    }
}
