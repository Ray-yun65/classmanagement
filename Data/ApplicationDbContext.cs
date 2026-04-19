using Microsoft.EntityFrameworkCore;
using 프로젝트관리.Models;

namespace 프로젝트관리.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<EducationClass> Classes => Set<EducationClass>();
    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<Meeting> Meetings => Set<Meeting>();
    public DbSet<Agenda> Agendas => Set<Agenda>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Session>()
            .HasOne(s => s.Class)
            .WithMany(c => c.Sessions)
            .HasForeignKey(s => s.ClassId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Attendance>()
            .HasOne(a => a.Session)
            .WithMany(s => s.Attendances)
            .HasForeignKey(a => a.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Agenda>()
            .HasOne(a => a.Meeting)
            .WithMany(m => m.Agendas)
            .HasForeignKey(a => a.MeetingId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Agenda>()
            .HasOne(a => a.RelatedClass)
            .WithMany()
            .HasForeignKey(a => a.RelatedClassId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Agenda>()
            .HasOne(a => a.RelatedSession)
            .WithMany()
            .HasForeignKey(a => a.RelatedSessionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
