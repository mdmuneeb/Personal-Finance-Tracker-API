using Microsoft.EntityFrameworkCore;
using SE.Models;
using SE.Services.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SE.Services.Services
{
    public class goalService: BaseService<Goal>, IGoal
    {
        public goalService(PersonalFinanceTrackerContext context): base(context)
        {
            
        }

        public async Task<Goal> AddGoal(Goal goalData)
        {
            var transaction = await BeginTransaction();
            try
            {
                _context.Goals.Add(goalData);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return goalData;

            }
            catch (Exception)
            {

                await transaction.RollbackAsync();
                return null;
            }
        }

        public async Task<List<Goal>> GetGoalByUserId(int userId)
        {
            var data = await _context.Goals.Where(x => x.UserId == userId).ToListAsync();
            return data;
        }

        public async Task<Goal> GetGoalByGoalId(int userId, int goalId)
        {
            var data = await _context.Goals.Where(x => x.UserId == userId && x.GoalId == goalId).FirstOrDefaultAsync();
            return data;
        }

        public async Task<Goal> UpdateGoalByGoalId(Goal newGoal)
        {
            var existingGoal = await GetGoalByGoalId((int)newGoal.UserId, newGoal.GoalId);
            var transaction = await BeginTransaction();

            try
            {
                if (existingGoal != null)
                {
                    if(newGoal.DeletedDate != null)
                    {
                        newGoal.DeleteTransaction = true;
                    }
                    _context.Entry(existingGoal).CurrentValues.SetValues(newGoal);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return newGoal;
                }
                return null;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return null;
            }
        }
        
    }
}
