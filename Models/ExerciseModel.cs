using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("workout_excercises")]
public class ExerciseModel
{
    [Key]
    [Column("id")]
    public int Id {get;set;}
    [Column("workout_id")]
    public int WorkoutId {get;set;}
    [Column("excercise_key")]
    public string ExerciseKey {get;set;} = null!;
    [Column("excercise_name")]
    public string ExerciseName {get;set;} = null!;
    [Column("order_index")]
    public int OrderIndex {get;set;}
    [Column("created_at")]
    public DateTime CreatedAt {get;private set;} = DateTime.UtcNow;

    /// Navigation Properties
    public WorkoutModel Workout {get;set;} = null!;
    public ICollection<SetModel> Sets {get;set;} = new List<SetModel>();
}
