using Hospital_System___Assignment.Models;

namespace Hospital_System___Assignment.Reposatries
{
    public interface IUnitOfWork : IDisposable
    {
        IReposatry<Doctor> Doctors { get; }
        IReposatry<BookAppointment> BookAppointments { get; }

        Task<int> SaveAsync();
    }
}
