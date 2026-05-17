
using GymBro.Models;
using Microsoft.EntityFrameworkCore;

public class GymBroDbContext : DbContext
{
    public GymBroDbContext(DbContextOptions<GymBroDbContext> options) : base(options)
    {
    }

    public DbSet<WorkoutModel> Workouts => Set<WorkoutModel>();
    public DbSet<ExerciseModel> Exercises => Set<ExerciseModel>();
    public DbSet<SetModel> Sets => Set<SetModel>();
    public DbSet<User> Users => Set<User>();
    public DbSet<NotificationModel> Notifications => Set<NotificationModel>();
    public DbSet<Friend> Friends => Set<Friend>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ExerciseModel>()
            .HasOne(e => e.Workout)
            .WithMany(w => w.Exercises)
            .HasForeignKey(e => e.WorkoutId);

        modelBuilder.Entity<SetModel>()
            .HasOne(s => s.Exercise)
            .WithMany(e => e.Sets)
            .HasForeignKey(s => s.WorkoutExerciseId);
    }
}
