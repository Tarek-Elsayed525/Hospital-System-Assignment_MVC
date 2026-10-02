using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_System___Assignment.Migrations
{
    /// <inheritdoc />
    public partial class add_data_table_BookAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO BookAppointments (PatientName, AppointmentDate, AppointmentTime, DoctorId) VALUES ('Ali Ahmed', '2026-10-05', '9:00 AM', 1),('Hassan Mohamed', '2026-10-05', '10:00 AM', 2),('Omar Khaled', '2026-10-06', '11:30 AM', 3),('Ibrahim Ehab', '2026-10-06', '1:00 PM', 4),('Bilal Ziad', '2026-10-07', '9:30 AM', 5),('Hamza Hossam', '2026-10-07', '2:00 PM', 6),('Yassin Tarek', '2026-10-08', '10:30 AM', 7),('Amir Adel', '2026-10-08', '3:00 PM', 8),('Nour Kamal', '2026-10-09', '11:00 AM', 9),('Ziad Nabil', '2026-10-09', '4:00 PM', 10);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM BookAppointments ");
        }
    }
}
