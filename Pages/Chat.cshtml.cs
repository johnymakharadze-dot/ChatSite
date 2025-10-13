using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatSite.Pages
{
    public class ChatModel : PageModel
    {
        [BindProperty]
        public string? Message { get; set; }

        public new string? Response { get; set; }


        public void OnPost()
        {
            if (!string.IsNullOrWhiteSpace(Message))
            {
                // მარტივი პასუხი — შეგიძლია აქ ლოგიკა შეცვალო მომავალში
                Response = $"შენ თქვი: {Message}";
            }
        }
    }
}
