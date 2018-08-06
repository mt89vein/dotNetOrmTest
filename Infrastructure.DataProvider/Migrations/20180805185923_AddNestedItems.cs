using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.DataProvider.Migrations
{
    public partial class AddNestedItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Document_DocumentId",
                table: "Attachment");

            migrationBuilder.DropIndex(
                name: "IX_Attachment_DocumentId",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Attachment");

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
                    table.PrimaryKey("PK_AttachmentLink", x => x.Id);
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
                    table.PrimaryKey("PK_NestedItem", x => x.Id);
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
                    table.PrimaryKey("PK_OneMoreNestedItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OneMoreNestedItem_NestedItem_NestedItemId",
                        column: x => x.NestedItemId,
                        principalTable: "NestedItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttachmentLink");

            migrationBuilder.DropTable(
                name: "OneMoreNestedItem");

            migrationBuilder.DropTable(
                name: "NestedItem");

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "Attachment",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Attachment",
                keyColumn: "Id",
                keyValue: 1,
                column: "DocumentId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Attachment",
                keyColumn: "Id",
                keyValue: 2,
                column: "DocumentId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Attachment",
                keyColumn: "Id",
                keyValue: 3,
                column: "DocumentId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_DocumentId",
                table: "Attachment",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Document_DocumentId",
                table: "Attachment",
                column: "DocumentId",
                principalTable: "Document",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
