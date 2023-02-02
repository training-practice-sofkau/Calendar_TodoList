using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoListSofka1.Migrations
{
    /// <inheritdoc />
    public partial class creandotablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos_Calendario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dia = table.Column<int>(type: "int", nullable: false),
                    Mes = table.Column<int>(type: "int", nullable: false),
                    Anio = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos_Calendario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tarea",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Responsible = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    State = table.Column<bool>(type: "bit", nullable: false),
                    CalendarModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarea", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tarea_Eventos_Calendario_CalendarModelId",
                        column: x => x.CalendarModelId,
                        principalTable: "Eventos_Calendario",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tarea_CalendarModelId",
                table: "Tarea",
                column: "CalendarModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tarea");

            migrationBuilder.DropTable(
                name: "Eventos_Calendario");
        }
    }
}
