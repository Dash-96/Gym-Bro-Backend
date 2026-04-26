namespace GymBroAspBackend.DTOs;

public class WorkoutDto
{
    public int UserId { get; set; }
    public string WorkoutType { get; set; } = null!;
    public string? WorkoutAlias { get; set; }
    // public DateTime? StartedAt { get; set; }
    // public DateTime? FinishedAt { get; set; }
    // public string? Notes { get; set; }
    // public DateTime CreatedAt { get; set; }
    // public DateTime? UpdatedAt { get; set; }
    public List<ExerciseDto> Exercises { get; set; } = [];
}

public class ExerciseDto
{
    public string ExerciseKey { get; set; } = null!;
    public string ExerciseName { get; set; } = null!;
    public int OrderIndex { get; set; }
    public List<SetDto> Sets { get; set; } = [];
}

public class SetDto
{
    public int SetNumber { get; set; }
    public int Reps { get; set; }
    public double Weight { get; set; }
}
