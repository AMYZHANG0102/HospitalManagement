using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                table: "PatientRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CurrentMedications",
                table: "PatientRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Diagnosis",
                table: "PatientRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LabTestsResults",
                table: "PatientRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MedicationAllergyInfo",
                table: "PatientRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PastMedicalHistory",
                table: "PatientRecords",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodType",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "CurrentMedications",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "Diagnosis",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "LabTestsResults",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "MedicationAllergyInfo",
                table: "PatientRecords");

            migrationBuilder.DropColumn(
                name: "PastMedicalHistory",
                table: "PatientRecords");
        }
    }
}
