using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Redis.OM;
using Redis.OM.Searching;
using RedisSample.Models;

namespace RedisSample.Pages;

public class AddNoteModel : PageModel
{
    private readonly ILogger<AddNoteModel> _logger;
    private readonly IRedisCollection<Note> _notes;

    [BindProperty]
    public Note? Note { get; set; }

    public AddNoteModel(ILogger<AddNoteModel> logger, RedisConnectionProvider redisConnection)
    {
        _logger = logger;
        _notes = redisConnection.RedisCollection<Note>();
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Note != null)
        {
            await _notes.InsertAsync(Note);
        }
        await _notes.SaveAsync();
        return Redirect("/");
    }
}
