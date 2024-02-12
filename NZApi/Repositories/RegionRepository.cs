using NZApi.Data;
using NZApi.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace NZApi.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZDbContext dbContext;
        public RegionRepository(NZDbContext dbContext)
        {
            this.dbContext = dbContext;
            
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetById(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.ReigionImageUrl = region.ReigionImageUrl;
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var deletedRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if(deletedRegion == null)
            {
                return null;
            }
            dbContext.Regions.Remove(deletedRegion);
            await dbContext.SaveChangesAsync();
            return deletedRegion;
           
        }
    }
}
