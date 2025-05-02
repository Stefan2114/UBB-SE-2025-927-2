﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialApp.Library.Interfaces
{
    public interface ICookingPageRepository
    {
        int GetUserIdByName(string firstName, string lastName);
        int GetCookingSkillIdByDescription(string description);
        void UpdateUserCookingSkill(int userId, int cookingSkillId);
    }
}
