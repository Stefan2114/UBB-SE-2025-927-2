using AppCommonClasses.DTOs;

namespace Server.Interfaces
{
    public interface IGroupControllerService
    {
        Task<IEnumerable<GroupDto>> GetAllGroupsAsync();

        Task<GroupDto?> GetGroupByIdAsync(long id);

        Task<IEnumerable<long>> GetUserIdsFromGroupAsync(long groupId);

        Task<IEnumerable<GroupDto>> GetGroupsForUserAsync(long userId);

        Task<GroupDto> CreateGroupAsync(GroupDto groupDto);

        Task<bool> UpdateGroupAsync(long id, GroupDto groupDto);

        Task<bool> DeleteGroupAsync(long id);
    }
}
