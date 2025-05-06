using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;

namespace Server.Interfaces
{
    public interface IGroupController
    {
        ActionResult<List<Group>> GetAllGroups();
        ActionResult<Group> GetGroupById(long id);
        ActionResult<List<UserModel>> GetUsersFromGroup(long id);
        ActionResult<List<Group>> GetGroupsForUser(long userId);
        IActionResult SaveGroup(Group group);
        IActionResult UpdateGroup(long id, [FromBody] Group group);
        IActionResult DeleteGroup(long id);
    }
}
