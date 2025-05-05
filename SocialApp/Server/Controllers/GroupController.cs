namespace Server.Controllers
{
    using AppCommonClasses.Interfaces;
    using AppCommonClasses.Models;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/groups")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            this.groupRepository = groupRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var groups = this.groupRepository.GetAllGroups();
            return this.Ok(groups);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var group = this.groupRepository.GetGroupById(id);
            if (group == null)
            {
                return this.NotFound();
            }

            return this.Ok(group);
        }

        [HttpGet("{id}/users")]
        public IActionResult GetUsersFromGroup(long id)
        {
            var users = this.groupRepository.GetUsersFromGroup(id);
            return this.Ok(users);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetGroupsForUser(long userId)
        {
            var groups = this.groupRepository.GetGroupsForUser(userId);
            return this.Ok(groups);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Group group)
        {
            this.groupRepository.SaveGroup(group);
            return this.Ok(group);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Group group)
        {
            this.groupRepository.UpdateGroup(id, group.Name, group.Image, group.Description, group.AdminId);
            return this.NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            this.groupRepository.DeleteGroupById(id);
            return this.NoContent();
        }
    }
}
