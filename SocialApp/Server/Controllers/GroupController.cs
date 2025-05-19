using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;
using AppCommonClasses.Interfaces;
using Server.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("groups")]
    public class GroupController : ControllerBase, IGroupController
    {
        private readonly IGroupService groupService;

        public GroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpGet]
        public ActionResult<List<Group>> GetAllGroups()
        {
            return Ok(groupService.GetAllGroups());
        }

        [HttpGet("{id}")]
        public ActionResult<Group> GetGroupById(long id)
        {
            return Ok(groupService.GetGroupById(id));
        }

        [HttpGet("{id}/users")]
        public ActionResult<List<User>> GetUsersFromGroup(long id)
        {
            return Ok(groupService.GetUsersFromGroup(id));
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<Group>> GetGroupsForUser(long userId)
        {
            return Ok(groupService.GetGroups(userId));
        }

        [HttpPost]
        public IActionResult SaveGroup([FromBody] Group group)
        {
            var newGroup = groupService.AddGroup(group.Name, group.Description, group.Image, group.AdminId);
            return Ok(newGroup);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGroup(long id, [FromBody] Group group)
        {
            groupService.UpdateGroup(id, group.Name, group.Description, group.Image, group.AdminId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGroup(long id)
        {
            groupService.DeleteGroup(id);
            return Ok();
        }
    }
}
