using AppCommonClasses.Models;

namespace AppCommonClasses.Interfaces
{
    public interface IMacrosRepository
    {
        Macros GetMacrosById(long id);
        List<Macros> GetAllMacros();
        List<Macros> GetMacrosByUserId(long userId);
        void SaveMacros(Macros macros);
        void UpdateMacrosById(long id, Macros updatedMacros);
        void DeleteMacrosById(long id);
    }
}