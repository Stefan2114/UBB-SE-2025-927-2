namespace Server.Repos
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Data.SqlClient;
    using AppCommonClasses.Models;
    using AppCommonClasses.Enums;
    using Server.Data;
    using AppCommonClasses.Interfaces;

    public class BodyMetricRepository : IBodyMetricRepository
    {
        private readonly SocialAppDbContext dbContext;

        public BodyMetricRepository(SocialAppDbContext context)
        {
            this.dbContext = context;
        }

        public void UpdateUserBodyMetrics(long userId, float weight, float height, float? TargetWeight)
        {
            var user = this.dbContext.Users.Find(userId);
            if (user != null)
            {
                user.Weight = weight;
                user.Height = height;
                user.TargetWeight = TargetWeight;
                this.dbContext.SaveChanges();
            }
        }
    }
}                                                                                                   