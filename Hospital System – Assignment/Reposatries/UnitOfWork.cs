using Hospital_System___Assignment.Date;
using Hospital_System___Assignment.Models;


namespace Hospital_System___Assignment.Reposatries
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDBContext _context;

        public UnitOfWork(ApplicationDBContext context)
        {
            _context = context;
            Doctors = new Reposatry<Doctor>(_context);
            BookAppointments = new Reposatry<BookAppointment>(_context);
        }

        public IReposatry<Doctor> Doctors { get; private set; }
        public IReposatry<BookAppointment> BookAppointments { get; private set; }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
