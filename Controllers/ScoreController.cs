using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class ScoreController: ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public ScoreController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }
    // Endpoint to create a new score
    [HttpPost]
    public async Task<IActionResult> CreateNewScore([FromBody] Score newScore)
    {
        try{

        _context.MaxScores.Add(newScore).State = EntityState.Added;
        await _context.SaveChangesAsync();

        return Ok("New Score Added");

        }catch  {

            return NotFound("error to Add New Score");
        }
    }

    // GET: api/Score
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Score>>> GetScores()
    {
        try{
            var scores = await _context.MaxScores.OrderBy(t => t.time).Take(50).ToListAsync();
            return Ok(new { scores });
        } catch{
            return NotFound("Getting scores failed");
        }
    }

    [HttpGet("user-Scores/{playerNameInScore}")]
    public async Task<IActionResult> GetUserTasks(string playerNameInScore)
    {
        try{
            var scores = await _context.MaxScores.Where(t => t.playerName == playerNameInScore).OrderBy(t => t.time).Take(50).ToListAsync();
            return Ok(scores);
        }
        catch
        {
            return NotFound("User Not Found");
        }        
    }

    [HttpGet("time-to-reset")]
    public IActionResult GetTimeToResetDatabase()
    {
        try
        {
            var now = DateTime.Now;

            // calculate the next reset time
            var nextReset = new DateTime(now.Year, now.Month, 1).AddMonths(1);

            // Calculate the time remaining until the next reset
            var timeRemaining = nextReset - now;

            return Ok(new
            {
                Days = timeRemaining.Days,
                Hours = timeRemaining.Hours,
                Minutes = timeRemaining.Minutes
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al calcular el tiempo para el reinicio: {ex.Message}");
        }
    }
}