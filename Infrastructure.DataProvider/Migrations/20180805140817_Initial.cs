using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.DataProvider.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Document",
                table => new
                {
                    Name = table.Column<string>(nullable: true),
                    DocumentType = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table => { table.PrimaryKey("PK_Document", x => x.Id); });

            migrationBuilder.CreateTable(
                "Attachment",
                table => new
                {
                    Path = table.Column<string>(nullable: true),
                    DocumentId = table.Column<int>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        "FK_Attachment_Document_DocumentId",
                        x => x.DocumentId,
                        "Document",
                        "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                "OtherDocument",
                table => new
                {
                    TestName = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherDocument", x => x.Id);
                    table.ForeignKey(
                        "FK_OtherDocument_Document_Id",
                        x => x.Id,
                        "Document",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "OtherDocumentItem",
                table => new
                {
                    Name = table.Column<string>(nullable: true),
                    OtherDocumentId = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherDocumentItem", x => x.Id);
                    table.ForeignKey(
                        "FK_OtherDocumentItem_OtherDocument_OtherDocumentId",
                        x => x.OtherDocumentId,
                        "OtherDocument",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "OtherDocumentPayment",
                table => new
                {
                    Total = table.Column<string>(nullable: true),
                    OtherDocumentId = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy",
                            SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherDocumentPayment", x => x.Id);
                    table.ForeignKey(
                        "FK_OtherDocumentPayment_OtherDocument_OtherDocumentId",
                        x => x.OtherDocumentId,
                        "OtherDocument",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                "Document",
                new[] {"Id", "Deleted", "DocumentType", "Name"},
                new object[,]
                {
                    {1, false, 0, "Первый документ первого типа"},
                    {2, true, 0, "Второй удаленный документ первого типа"},
                    {3, false, 0, "Третий документ первого типа"},
                    {4, false, 0, "Четвертый документ первого типа"}
                });

            migrationBuilder.InsertData(
                "Attachment",
                new[] {"Id", "Deleted", "DocumentId", "Path"},
                new object[,]
                {
                    {1, false, 1, "some/path"},
                    {2, false, 1, "some/another/path"},
                    {3, true, 1, "some/either/path"}
                });

            migrationBuilder.InsertData(
                "OtherDocument",
                new[] {"Id", "Deleted", "TestName"},
                new object[,]
                {
                    {1, false, "Первый документ первого типа"},
                    {2, true, "Второй документ первого типа"},
                    {3, false, "Третий удаленный документ первого типа"},
                    {4, false, "Четвертый документ первого типа"}
                });

            migrationBuilder.InsertData(
                "OtherDocumentItem",
                new[] {"Id", "Deleted", "Name", "OtherDocumentId"},
                new object[,]
                {
                    {1, false, "item 1 1", 1},
                    {2, true, "item 1 2", 1},
                    {3, false, "item 2 1", 2},
                    {4, true, "item 2 2", 2},
                    {5, false, "item 3 1", 3},
                    {6, true, "item 3 2", 3},
                    {7, false, "item 4 1", 4},
                    {8, true, "item 4 2", 4}
                });

            migrationBuilder.InsertData(
                "OtherDocumentPayment",
                new[] {"Id", "Deleted", "OtherDocumentId", "Total"},
                new object[,]
                {
                    {1, false, 1, "150"},
                    {2, true, 1, "25"},
                    {3, false, 2, "450"},
                    {4, true, 2, "132"},
                    {5, false, 3, "444"},
                    {6, true, 3, "521"},
                    {7, false, 4, "421"},
                    {8, true, 4, "4444"}
                });

            migrationBuilder.CreateIndex(
                "IX_Attachment_DocumentId",
                "Attachment",
                "DocumentId");

            migrationBuilder.CreateIndex(
                "IX_OtherDocumentItem_OtherDocumentId",
                "OtherDocumentItem",
                "OtherDocumentId");

            migrationBuilder.CreateIndex(
                "IX_OtherDocumentPayment_OtherDocumentId",
                "OtherDocumentPayment",
                "OtherDocumentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "Attachment");

            migrationBuilder.DropTable(
                "OtherDocumentItem");

            migrationBuilder.DropTable(
                "OtherDocumentPayment");

            migrationBuilder.DropTable(
                "OtherDocument");

            migrationBuilder.DropTable(
                "Document");
        }
    }
}