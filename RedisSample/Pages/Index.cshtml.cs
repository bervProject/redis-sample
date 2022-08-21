using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Redis.OM;
using Redis.OM.Searching;
using RedisSample.Models;

namespace RedisSample.Pages;

public class IndexModel : PageModel
{
  private readonly ILogger<IndexModel> _logger;
  private readonly IRedisCollection<Note> _notes;
  public List<Note> NoteList { get; set; } = new List<Note>();


  public IndexModel(ILogger<IndexModel> logger, RedisConnectionProvider redisConnection)
  {
    _logger = logger;
    _notes = redisConnection.RedisCollection<Note>();
  }

  public async Task OnGetAsync()
  {
    var notes = await _notes.ToListAsync();
    NoteList.AddRange(notes);
  }

  public async Task<IActionResult> OnPostDeleteNoteAsync(string id)
  {
    var note = await _notes.FindByIdAsync(id);

    _logger.LogDebug($"...... data: {note}");

    if (note == null)
    {
      return NotFound();
    }

    await _notes.DeleteAsync(note);
    await _notes.SaveAsync();

    return RedirectToPage();
  }
}
