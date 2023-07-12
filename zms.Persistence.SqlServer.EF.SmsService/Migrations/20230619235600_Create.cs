using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zms.Persistence.SqlServer.EF.SmsService.Migrations
{
    /// <inheritdoc />
    public partial class Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "MessageHiLo");

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "Первичный ключ"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Дата создания"),
                    Category_Value = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Код категории сообщения"),
                    Category_Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Наименование категории сообщения"),
                    SendingPeriod_TimeStart = table.Column<TimeSpan>(type: "time", nullable: false, comment: "Начало периода времени отправки"),
                    SendingPeriod_TimeEnd = table.Column<TimeSpan>(type: "time", nullable: false, comment: "Начало периода времени отправки"),
                    Recipient_Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Имя получателя"),
                    Recipient_Phone_Value = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Номер телефона"),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Имя отправителя"),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Тема сообщения"),
                    Status_Value = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Код статуса"),
                    Status_Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Наименование статуса"),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Внешний идентификатор"),
                    ProcessedDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Дата обработки"),
                    SendingDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Дата отправки"),
                    Provider_Name = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Наименование провайдера отправки"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Комментарий"),
                    SmsCount = table.Column<int>(type: "int", nullable: false, comment: "Количество потраченных СМС на сообщение")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                },
                comment: "Таблица СМС сообщений");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropSequence(
                name: "MessageHiLo");
        }
    }
}
