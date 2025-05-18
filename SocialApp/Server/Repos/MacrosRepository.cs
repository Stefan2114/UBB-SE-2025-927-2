using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using Server.Data;

namespace Server.Repos
{
    public class MacrosRepository : IMacrosRepository
    {
        private readonly SocialAppDbContext dbContext;

        public MacrosRepository(SocialAppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Macros GetMacrosById(long id)
        {
            return this.dbContext.Macros.First(m => m.DailyMealId == id);
        }

        public List<Macros> GetAllMacros()
        {
            return this.dbContext.Macros.ToList();
        }

        public List<Macros> GetMacrosByUserId(long userId)
        {
            return this.dbContext.Macros
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.DateEaten)
                .ToList();
        }

        public void SaveMacros(Macros macros)
        {
            this.dbContext.Macros.Add(macros);
            this.dbContext.SaveChanges();
        }

        public void UpdateMacrosById(long id, Macros updatedMacros)
        {
            var macros = this.dbContext.Macros.Find(id);
            if (macros != null)
            {
                macros.TotalProtein = updatedMacros.TotalProtein;
                macros.TotalCarbohydrates = updatedMacros.TotalCarbohydrates;
                macros.TotalFat = updatedMacros.TotalFat;
                macros.TotalCalories = updatedMacros.TotalCalories;
                macros.DateEaten = updatedMacros.DateEaten;
                this.dbContext.SaveChanges();
            }
        }

        public void DeleteMacrosById(long id)
        {
            var macros = this.dbContext.Macros.Find(id);
            if (macros != null)
            {
                this.dbContext.Macros.Remove(macros);
                this.dbContext.SaveChanges();
            }
        }
    }
}
