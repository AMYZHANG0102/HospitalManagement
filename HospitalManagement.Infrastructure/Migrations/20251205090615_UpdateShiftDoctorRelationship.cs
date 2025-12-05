using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateShiftDoctorRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorShift");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "Shifts",
                newName: "StartDateTime");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "Shifts",
                newName: "EndDateTime");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Shifts",
                newName: "DoctorId");

            migrationBuilder.AddColumn<bool>(
                name: "DoctorIsUnavailable",
                table: "Appointments",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Shifts_DoctorId",
                table: "Shifts",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shifts_AspNetUsers_DoctorId",
                table: "Shifts",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shifts_AspNetUsers_DoctorId",
                table: "Shifts");

            migrationBuilder.DropIndex(
                name: "IX_Shifts_DoctorId",
                table: "Shifts");

            migrationBuilder.DropColumn(
                name: "DoctorIsUnavailable",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "Shifts",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "EndDateTime",
                table: "Shifts",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "DoctorId",
                table: "Shifts",
                newName: "Date");

            migrationBuilder.CreateTable(
                name: "DoctorShift",
                columns: table => new
                {
                    DoctorsId = table.Column<string>(type: "TEXT", nullable: false),
                    ShiftsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorShift", x => new { x.DoctorsId, x.ShiftsId });
                    table.ForeignKey(
                        name: "FK_DoctorShift_AspNetUsers_DoctorsId",
                        column: x => x.DoctorsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorShift_Shifts_ShiftsId",
                        column: x => x.ShiftsId,
                        principalTable: "Shifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorShift_ShiftsId",
                table: "DoctorShift",
                column: "ShiftsId");
        }
    }
}
