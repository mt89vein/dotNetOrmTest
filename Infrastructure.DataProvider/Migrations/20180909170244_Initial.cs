using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.DataProvider.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Path = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: true),
                    DocumentType = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                });

            migrationBuilder.CreateTable(
                name: "AttachmentLink",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DocumentId = table.Column<int>(nullable: false),
                    AttachmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentLink", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_AttachmentLink_Attachment_AttachmentId",
                        column: x => x.AttachmentId,
                        principalTable: "Attachment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttachmentLink_Document_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Document",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OtherDocument",
                columns: table => new
                {
                    TestName = table.Column<string>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherDocument", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_OtherDocument_Document_Id",
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
                    Deleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherDocumentItem", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
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
                    Total = table.Column<string>(nullable: true),
                    OtherDocumentId = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherDocumentPayment", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_OtherDocumentPayment_OtherDocument_OtherDocumentId",
                        column: x => x.OtherDocumentId,
                        principalTable: "OtherDocument",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NestedItem",
                columns: table => new
                {
                    NestedItemName = table.Column<string>(nullable: true),
                    OtherDocumentItemId = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NestedItem", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_NestedItem_OtherDocumentItem_OtherDocumentItemId",
                        column: x => x.OtherDocumentItemId,
                        principalTable: "OtherDocumentItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OneMoreNestedItem",
                columns: table => new
                {
                    OneMoreNestedItemName = table.Column<string>(nullable: true),
                    NestedItemId = table.Column<int>(nullable: false),
                    Deleted = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneMoreNestedItem", x => x.Id)
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_OneMoreNestedItem_NestedItem_NestedItemId",
                        column: x => x.NestedItemId,
                        principalTable: "NestedItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Attachment",
                columns: new[] { "Id", "Deleted", "Path" },
                values: new object[,]
                {
                    { 1, false, "some/path" },
                    { 2, false, "some/another/path" },
                    { 3, true, "some/either/path" }
                });

            migrationBuilder.InsertData(
                table: "Document",
                columns: new[] { "Id", "Deleted", "DocumentType", "Name" },
                values: new object[,]
                {
                    { 1, false, 0, "Первый документ первого типа" },
                    { 2, true, 0, "Второй удаленный документ первого типа" },
                    { 3, false, 0, "Третий документ первого типа" },
                    { 4, false, 0, "Четвертый документ первого типа" }
                });

            migrationBuilder.InsertData(
                table: "AttachmentLink",
                columns: new[] { "Id", "AttachmentId", "DocumentId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 2 },
                    { 4, 1, 2 },
                    { 5, 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "OtherDocument",
                columns: new[] { "Id", "Deleted", "TestName" },
                values: new object[,]
                {
                    { 1, false, "Первый документ первого типа" },
                    { 2, true, "Второй документ первого типа" },
                    { 3, false, "Третий удаленный документ первого типа" },
                    { 4, false, "Четвертый документ первого типа" }
                });

            migrationBuilder.InsertData(
                table: "OtherDocumentItem",
                columns: new[] { "Id", "Deleted", "Name", "OtherDocumentId" },
                values: new object[,]
                {
                    { 1, false, "item 1 1", 1 },
                    { 2, true, "item 1 2", 1 },
                    { 3, false, "item 2 1", 2 },
                    { 4, true, "item 2 2", 2 },
                    { 5, false, "item 3 1", 3 },
                    { 6, true, "item 3 2", 3 },
                    { 7, false, "item 4 1", 4 },
                    { 8, true, "item 4 2", 4 }
                });

            migrationBuilder.InsertData(
                table: "OtherDocumentPayment",
                columns: new[] { "Id", "Deleted", "OtherDocumentId", "Total" },
                values: new object[,]
                {
                    { 1, false, 1, "150" },
                    { 2, true, 1, "25" },
                    { 3, false, 2, "450" },
                    { 4, true, 2, "132" },
                    { 5, false, 3, "444" },
                    { 6, true, 3, "521" },
                    { 7, false, 4, "421" },
                    { 8, true, 4, "4444" }
                });

            migrationBuilder.InsertData(
                table: "NestedItem",
                columns: new[] { "Id", "Deleted", "NestedItemName", "OtherDocumentItemId" },
                values: new object[,]
                {
                    { 1, false, "nested item name 1", 1 },
                    { 2, false, "nested item name 2", 1 },
                    { 3, false, "nested item name 3", 2 },
                    { 4, false, "nested item name 4", 2 },
                    { 5, false, "nested item name 5", 3 },
                    { 6, false, "nested item name 6", 3 },
                    { 7, false, "nested item name 7", 4 },
                    { 8, false, "nested item name 8", 4 },
                    { 9, false, "nested item name 9", 8 }
                });

            migrationBuilder.InsertData(
                table: "OneMoreNestedItem",
                columns: new[] { "Id", "Deleted", "NestedItemId", "OneMoreNestedItemName" },
                values: new object[,]
                {
                    { 1, false, 1, "one more nested item name 1" },
                    { 2, false, 1, "one more nested item name 2" },
                    { 3, false, 2, "one more nested item name 3" },
                    { 4, false, 2, "one more nested item name 4" },
                    { 5, false, 3, "one more nested item name 5" },
                    { 6, false, 3, "one more nested item name 6" },
                    { 7, false, 4, "one more nested item name 7" },
                    { 8, false, 4, "one more nested item name 8" },
                    { 9, false, 5, "one more nested item name 9" },
                    { 10, false, 5, "one more nested item name 10" },
                    { 11, false, 6, "one more nested item name 11" },
                    { 12, false, 6, "one more nested item name 12" },
                    { 13, false, 7, "one more nested item name 13" },
                    { 14, false, 7, "one more nested item name 14" },
                    { 15, false, 8, "one more nested item name 15" },
                    { 16, false, 8, "one more nested item name 16" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentLink_AttachmentId",
                table: "AttachmentLink",
                column: "AttachmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentLink_DocumentId",
                table: "AttachmentLink",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_NestedItem_OtherDocumentItemId",
                table: "NestedItem",
                column: "OtherDocumentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OneMoreNestedItem_NestedItemId",
                table: "OneMoreNestedItem",
                column: "NestedItemId");

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
                name: "AttachmentLink");

            migrationBuilder.DropTable(
                name: "OneMoreNestedItem");

            migrationBuilder.DropTable(
                name: "OtherDocumentPayment");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "NestedItem");

            migrationBuilder.DropTable(
                name: "OtherDocumentItem");

            migrationBuilder.DropTable(
                name: "OtherDocument");

            migrationBuilder.DropTable(
                name: "Document");
        }
    }
}
