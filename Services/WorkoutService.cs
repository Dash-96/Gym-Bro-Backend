using GymBroAspBackend.DTOs;
using Microsoft.EntityFrameworkCore;

public class WorkoutService
{
    private GymBroDbContext _context;
    public WorkoutService(GymBroDbContext context)
    {
        _context = context;
    }

    async public Task<IEnumerable<WorkoutDto>> GetWorkoutsAsync()
    {
        var workoutsList = _context.Workouts.Select( w => new WorkoutDto
        {
            UserId = w.UserId,
            WorkoutType = w.WorkoutType ,
            WorkoutAlias = w.WorkoutAlias,
            Exercises = w.Exercises.Select(exercise => new ExerciseDto
            {
                 ExerciseKey = exercise.ExerciseKey ,
                  ExerciseName = exercise.ExerciseName
            }).ToList()
        }).ToListAsync();

        return await workoutsList;
    }

    async public Task CreateWorkout(WorkoutDto dto)
    {
        //! TODO: First check if workout wasnt added already, check by -> created_at + used_id 
        // var workout = _context.Workouts.Where(w => w.CreatedAt == dto.)

        WorkoutModel workout = new()
        {
            UserId = dto.UserId,
            WorkoutType = dto.WorkoutType,
            Exercises = [..dto.Exercises.Select(e => new ExerciseModel
            {
                ExerciseKey = e.ExerciseKey,
                ExerciseName = e.ExerciseName,
                OrderIndex = e.OrderIndex,
                Sets = [..e.Sets.Select(s => new SetModel
                {
                    SetNumber = s.SetNumber,
                    Reps = s.Reps,
                    Weight = s.Weight
                })]
            })]
        };

        await _context.Workouts.AddAsync(workout);
        await _context.SaveChangesAsync();
    }
}
