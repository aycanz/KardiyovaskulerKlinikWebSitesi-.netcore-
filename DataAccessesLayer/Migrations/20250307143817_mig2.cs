using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessesLayer.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uzmanlik",
                table: "Doctors",
                newName: "Specialty");

            migrationBuilder.RenameColumn(
                name: "Telefon",
                table: "Doctors",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Soyad",
                table: "Doctors",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Hastane",
                table: "Doctors",
                newName: "Hospital");

            migrationBuilder.RenameColumn(
                name: "Ad",
                table: "Doctors",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "DoktorId",
                table: "Doctors",
                newName: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Specialty",
                table: "Doctors",
                newName: "Uzmanlik");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Doctors",
                newName: "Telefon");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Doctors",
                newName: "Soyad");

            migrationBuilder.RenameColumn(
                name: "Hospital",
                table: "Doctors",
                newName: "Hastane");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Doctors",
                newName: "Ad");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Doctors",
                newName: "DoktorId");
        }
    }
}
