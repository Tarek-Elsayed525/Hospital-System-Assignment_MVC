using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_System___Assignment.Migrations
{
    /// <inheritdoc />
    public partial class add_data_table_doctor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Doctors (DoctorName, Specialization, DoctorImage) VALUES ('Dr. Ahmed Mahmoud', 'Cardiology', '1.png'),('Dr. Rami Khaled', 'Neurology', '2.png'),('Dr. Khaled Hassan', 'Orthopedics', '3.png'),('Dr. Tarek Ehab', 'Pediatrics', '4.png'),('Dr. Youssef Ibrahim', 'Dermatology', '5.png'),('Dr. Ziad Hossam', 'Cardiology', '6.png'),('Dr. Mahmoud Youssef', 'Oncology', '7.png'),('Dr. Amr Adel', 'Neurology', '8.png'),('Dr. Mostafa Kamal', 'Orthopedics', '9.png'),('Dr. Karim Nabil', 'Pediatrics', '10.png');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Doctors ");
        }
    }
}
