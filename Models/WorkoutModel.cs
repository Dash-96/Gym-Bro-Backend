using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;


[Table("workouts")]
public class WorkoutModel
{
    [Key]
    [Column("id")]
    public int Id {get;set;}
    [Column("user_id")]
    public int UserId {get;set;}
    [Column("workout_type")]
    public string WorkoutType {get;set;} = null!;
    [Column("workout_alias")]
    public string? WorkoutAlias{get;set;} = "";
    [Column("started_at")]
    public DateTime StartedAt {get;set;} = DateTime.UtcNow;
    [Column("finished_at")]
    public DateTime? FinishedAt {get;set;}
    [Column("notes")]
    public string? Notes {get;set;} = "";
    [Column("created_at")]
    public DateTime CreatedAt {get;set;} = DateTime.UtcNow;
    [Column("updated_at")]
    public DateTime? UpdatedAt {get;set;}

    /// Navigation Properties

    public ICollection<ExerciseModel> Exercises {get;set;} = new List<ExerciseModel>();

}
