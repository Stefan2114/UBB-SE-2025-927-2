using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AppCommonClasses.Models;

namespace MealSocialServerMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AppCommonClasses.Models.GroceryIngredient> GroceryIngredient { get; set; } = default!;
    }
}
