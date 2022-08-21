using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Redis.OM;
using Redis.OM.Searching;
using RedisSample.Models;

namespace RedisSample.Pages;

public class EditNoteModel : PageModel
{
  private readonly ILogger<AddNoteModel> _logger;
  private readonly IRedisCollection<Note> _notes;

  [BindProperty]
  public Note? Note { get; set; }

  public EditNoteModel(ILogger<AddNoteModel> logger, RedisConnectionProvider redisConnection)
  {
    _logger = logger;
    _notes = redisConnection.RedisCollection<Note>();
  }

  public async Task<IActionResult> OnGetAsync(string id)
  {
    Note = await _notes.FindByIdAsync(id);
    if (Note == null)
    {
      return NotFound();
    }
    return Page();
  }

  public async Task<IActionResult> OnPostAsync()
  {
    if (!ModelState.IsValid)
    {
      return Page();
    }

    if (Note != null)
    {
      await _notes.UpdateAsync(Note);
    }
    await _notes.SaveAsync();
    return Redirect("/");
  }
}
