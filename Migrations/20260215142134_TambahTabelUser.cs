using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _2026_PraPBL_Backend.Migrations
{
    /// <inheritdoc />
    public partial class TambahTabelUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Capacity", "Description", "Name" },
                values: new object[] { 100, "Lantai 2", "Auditorium" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Capacity", "Description", "Name" },
                values: new object[] { 20, "Lantai 1", "Sekretariat Bersama" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Capacity", "Description" },
                values: new object[] { 30, "Gedung D3" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Capacity", "Description", "Name" },
                values: new object[] { 200, "Pascasarjana Lt 6", "Aula PENS" });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Capacity", "DeletedAt", "Description", "IsAvailable", "Name" },
                values: new object[] { 5, 200, null, "Gedung D3 Lt 1", true, "Mini Teater" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "password123", "Admin", "admin" },
                    { 2, "user123", "User", "fazel" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Capacity", "Description", "Name" },
                values: new object[] { 30, "Lantai 2 Gedung D4", "Lab SCADA" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Capacity", "Description", "Name" },
                values: new object[] { 100, "Lantai 1 Gedung TC", "Ruang Teater" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Capacity", "Description" },
                values: new object[] { 25, "Lantai 3 Gedung D3" });

            migrationBuilder.UpdateData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Capacity", "Description", "Name" },
                values: new object[] { 500, "Lantai 1 Gedung D4", "Aula Pens" });
        }
    }
}
