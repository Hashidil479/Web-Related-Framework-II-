using Employee_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Employee_Management_System.Data
{
    public class Assignment:DbContext
    {
        public Assignment(DbContextOptions<Assignment> options): base(options) { }
        public DbSet<Eployee>Employees { get; set; }
    }
}
