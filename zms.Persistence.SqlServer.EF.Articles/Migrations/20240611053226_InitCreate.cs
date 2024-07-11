using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zms.Persistence.SqlServer.EF.Articles.Migrations
{
    /// <inheritdoc />
    public partial class InitCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "ArticleHiLo");

            migrationBuilder.CreateSequence(
                name: "AttachmentHiLo");

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Первичный ключ"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата создания"),
                    Title_Value = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Заголовок"),
                    Content_Value = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Содержимое"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                },
                comment: "Таблица статей");

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Первичный ключ"),
                    ArticleId = table.Column<long>(type: "bigint", nullable: true, comment: "Внешний ключ"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата создания"),
                    Type_Value = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Код типа вложения"),
                    Type_Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Наименование типа вложения"),
                    Name_Value = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Наименование"),
                    StorageId = table.Column<long>(type: "bigint", nullable: false, comment: "Идентификатор в хранилище"),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachment_Article_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Article",
                        principalColumn: "Id");
                },
                comment: "Таблица вложений");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_ArticleId",
                table: "Attachment",
                column: "ArticleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropSequence(
                name: "ArticleHiLo");

            migrationBuilder.DropSequence(
                name: "AttachmentHiLo");
        }
    }
}
