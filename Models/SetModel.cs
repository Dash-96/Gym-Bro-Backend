using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("excercise_sets")]
public class SetModel
{
    [Key]
    [Column("id")]
    public int Id {get;set;}
    [Column("workout_excercise_id")]
    public int WorkoutExerciseId {get;set;}
    [Column("set_number")]
    public int SetNumber {get;set;}
    [Column("reps")]
    public int Reps {get;set;}
    [Column("weight")]
    public double Weight {get;set;}
    [Column("created_at")]
    public DateTime CreatedAt {get;private set;} = DateTime.UtcNow;

    /// Navigation Properties
    [ForeignKey(nameof(WorkoutExerciseId))]
    public ExerciseModel Exercise {get;set;} = null!;

}
