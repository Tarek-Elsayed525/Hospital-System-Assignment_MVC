using Microsoft.EntityFrameworkCore;

namespace Hospital_System___Assignment.Date
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<Models.Doctor> Doctors { get; set; }
        public DbSet<Models.BookAppointment> BookAppointments { get; set; }

        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-8F377B2;initial catalog=HospitalSystemDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True");
        }
    }
}
