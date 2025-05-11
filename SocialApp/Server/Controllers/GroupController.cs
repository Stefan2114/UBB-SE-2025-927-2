using AppCommonClasses.Interfaces;
using AppCommonClasses.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Interfaces;

namespace Server.Controllers
{
    [ApiController]
    [Route("groups")]
    public class GroupController : ControllerBase, IGroupController
    {
        private readonly IGroupRepository groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        [HttpGet]
        public ActionResult<List<Group>> GetAllGroups()
        {
            return this.groupRepository.GetAllGroups();
        }

        [HttpGet("{id}")]
        public ActionResult<Group> GetGroupById(long id)
        {
            return this.groupRepository.GetGroupById(id);
        }

        [HttpGet("{id}/users")]
        public ActionResult<List<User>> GetUsersFromGroup(long id)
        {
            return this.groupRepository.GetUsersFromGroup(id);
        }

        [HttpGet("user/{userId}")]
        public ActionResult<List<Group>> GetGroupsForUser(long userId)
        {
            return this.groupRepository.GetGroupsForUser(userId);
        }

        [HttpPost]
        public IActionResult SaveGroup([FromBody] Group group)
        {
            this.groupRepository.SaveGroup(group);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGroup(long id, [FromBody] Group group)
        {
            this.groupRepository.UpdateGroup(id, group.Name, group.Image, group.Description, group.AdminId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGroup(long id)
        {
            this.groupRepository.DeleteGroupById(id);
            return Ok();
        }
    }
}
