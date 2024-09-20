using Microsoft.AspNetCore.Identity;
namespace NewsPaperAuthExample.Entities
{
    public class User: IdentityUser<int>
    {
        public string Name { get; set; }
    }
}
