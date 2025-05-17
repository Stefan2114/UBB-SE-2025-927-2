using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCommonClasses.Interfaces
{
    public interface ICalorieService
    {
        double GetGoal(long userId);

        double GetFood(long userId);

        double GetExercise(long userId);
    }
}
