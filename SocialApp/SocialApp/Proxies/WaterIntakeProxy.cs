namespace SocialApp.Proxies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using SocialApp.Interfaces;

    public class WaterIntakeProxy : IWaterIntakeService
    {
        private readonly HttpClient httpClient;

        public WaterIntakeProxy(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.httpClient.BaseAddress = new Uri("https://localhost:7106/users/")
        }

        public void AddUserIfNotExists(int userId)
        {
            throw new NotImplementedException();
        }

        public float GetWaterIntake(int userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveWater300(int userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveWater400(int userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveWater500(int userId)
        {
            throw new NotImplementedException();
        }

        public void RemoveWater750(int userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateWaterIntake(int userId, float newIntake)
        {
            throw new NotImplementedException();
        }
    }
}
