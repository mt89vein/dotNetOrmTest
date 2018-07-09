using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataProviced.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: true),
                    DocumentType = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OtherDocument",
                columns: table => new
                {
                    TestName = table.Column<string>(nullable: true),
                    PublicationEvent_UserId = table.Column<int>(nullable: true),
                    PublicationEvent_Date = table.Column<DateTime>(nullable: true),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherDocument_Document_Id",
                        column: x => x.Id,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecondDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    DocumentSigner = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SecondDocument_Document_Id",
                        column: x => x.Id,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtherDocumentItem",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: true),
                    OtherDocumentId = table.Column<int>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherDocumentItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherDocumentItem_OtherDocument_OtherDocumentId",
                        column: x => x.OtherDocumentId,
                        principalTable: "OtherDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtherDocumentPayment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Total = table.Column<string>(nullable: true),
                    OtherDocumentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherDocumentPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherDocumentPayment_OtherDocument_OtherDocumentId",
                        column: x => x.OtherDocumentId,
                        principalTable: "OtherDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtherDocumentItem_OtherDocumentId",
                table: "OtherDocumentItem",
                column: "OtherDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_OtherDocumentPayment_OtherDocumentId",
                table: "OtherDocumentPayment",
                column: "OtherDocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtherDocumentItem");

            migrationBuilder.DropTable(
                name: "OtherDocumentPayment");

            migrationBuilder.DropTable(
                name: "SecondDocument");

            migrationBuilder.DropTable(
                name: "OtherDocument");

            migrationBuilder.DropTable(
                name: "Document");
        }
    }
}
