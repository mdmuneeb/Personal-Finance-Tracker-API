using SE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE.Services.Inteface
{
    public interface IGoal
    {
        Task<Goal> AddGoal(Goal goalData);
        Task<List<Goal>> GetGoalByUserId(int userId);
        Task<Goal> UpdateGoalByGoalId(Goal newGoal);
        Task<Goal> deleteGoal(int userId, int gId);
    }
}
