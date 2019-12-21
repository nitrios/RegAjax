using Microsoft.EntityFrameworkCore.Migrations;

namespace RegAjax.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(nullable: false),
                    SecondName = table.Column<string>(nullable: false),
                    Phone = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Variants",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    QuestionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Variants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Variants_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RegistrationId = table.Column<long>(nullable: false),
                    VariantId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Registrations_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "Registrations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Variants_VariantId",
                        column: x => x.VariantId,
                        principalTable: "Variants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1L, "Какой цвет вам больше нравится?" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2L, "Какой напиток вы предпочитаете" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "PasswordHash" },
                values: new object[] { 1L, "admin", "8C6976E5B5410415BDE908BD4DEE15DFB167A9C873FC4BB8A81F6F2AB448A918" });

            migrationBuilder.InsertData(
                table: "Variants",
                columns: new[] { "Id", "Name", "QuestionId" },
                values: new object[] { 1L, "Синий", 1L });

            migrationBuilder.InsertData(
                table: "Variants",
                columns: new[] { "Id", "Name", "QuestionId" },
                values: new object[] { 2L, "Желтый", 1L });

            migrationBuilder.InsertData(
                table: "Variants",
                columns: new[] { "Id", "Name", "QuestionId" },
                values: new object[] { 3L, "Красный", 1L });

            migrationBuilder.InsertData(
                table: "Variants",
                columns: new[] { "Id", "Name", "QuestionId" },
                values: new object[] { 4L, "Чай", 2L });

            migrationBuilder.InsertData(
                table: "Variants",
                columns: new[] { "Id", "Name", "QuestionId" },
                values: new object[] { 5L, "Кофе", 2L });

            migrationBuilder.InsertData(
                table: "Variants",
                columns: new[] { "Id", "Name", "QuestionId" },
                values: new object[] { 6L, "Сок", 2L });

            migrationBuilder.InsertData(
                table: "Variants",
                columns: new[] { "Id", "Name", "QuestionId" },
                values: new object[] { 7L, "Вода", 2L });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_RegistrationId",
                table: "Answers",
                column: "RegistrationId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_VariantId",
                table: "Answers",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_Variants_QuestionId",
                table: "Variants",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Registrations");

            migrationBuilder.DropTable(
                name: "Variants");

            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
