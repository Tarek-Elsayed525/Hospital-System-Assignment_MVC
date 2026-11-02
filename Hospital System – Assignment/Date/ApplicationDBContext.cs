using Microsoft.EntityFrameworkCore;

namespace Hospital_System___Assignment.Date
{
    public class ApplicationDBContext : DbContext
    {

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Models.Doctor> Doctors { get; set; }
        public DbSet<Models.BookAppointment> BookAppointments { get; set; }

    }
}
