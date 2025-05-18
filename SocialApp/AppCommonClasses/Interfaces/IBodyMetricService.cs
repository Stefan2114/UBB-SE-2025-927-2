using System;

namespace AppCommonClasses.Interfaces
{
    public interface IBodyMetricService
    {
        void UpdateUserBodyMetrics(string username, float weight, float height, float? targetWeight);
    }
}
