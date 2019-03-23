using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Database.Interfaces;
using Database.Redundancy;
using Microsoft.EntityFrameworkCore;

namespace Database.Repository_Implementations
{
    public class BarRepository : Repository<Bar>, IBarRepository
    {
        public BarRepository(DbContext _dbcontext) : base(_dbcontext)
        {
        }

            /*
            Der returneres ikke IQueryable da det ikke skal være muligt i lagene længere oppe, at lave queries på det vi sender.
            Det er vores job at query databasen, samt sørge for at der ikke bliver queriet på ting som der ikke er ment til at kunne
            queries på.
            */
        public IEnumerable<Bar> GetXBars(int from, int to)
        {
            return _dbContext.Set<Bar>().OrderBy(c => c.BarName).Skip(from).Take(to).AsEnumerable().ToList();
        }

        public IEnumerable<Bar> GetBestBars()
        {
            return _dbContext.Set<Bar>().OrderByDescending(b => b.AvgRating).AsEnumerable().ToList();
        }

        public IEnumerable<Bar> GetWorstBars()
        {
            var worstBarList = _dbContext.Set<Bar>().OrderByDescending(b => b.AvgRating).AsEnumerable().ToList();
            worstBarList.Reverse();
            return worstBarList;
        }
    }
}
