using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZApi.Data;
using NZApi.Models.Domain;
using NZApi.Models.DTO;
using NZApi.Repositories;

namespace NZApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(NZDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        [HttpGet]
        //public IActionResult GetAll()
        public async Task<IActionResult> GetAll()
        {
            // Get Data From Database - Domain models
            //var regionsDomain = dbContext.Regions.ToList();
            ///var regionsDomain = await dbContext.Regions.ToListAsync();
            var regionsDomain = await regionRepository.GetAllAsync();
            //Map Domain Models to DTOs
            var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    ReigionImageUrl = regionDomain.ReigionImageUrl
                });
            }
            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        //public IActionResult GetById([FromRoute] Guid id) {
        public async Task<IActionResult> GetById([FromRoute] Guid id)
            {
            //Get Region Domain Model from db
            //var regionDomain = dbContext.Regions.Find(id);
            //var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            ///var regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomain = await regionRepository.GetById(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            // map Region Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                ReigionImageUrl = regionDomain.ReigionImageUrl
            };
            return Ok(regionDto);
        }

        [HttpPost]
        //public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)    
        {
            //Map or Covert Dto to domain model
            var regionDomainModel =  new Region 
            { 
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                ReigionImageUrl = addRegionRequestDto.ReigionImageUrl
            };
            //user Domain Model to create region
            //dbContext.Regions.Add(regionDomainModel);
            ///await dbContext.Regions.AddAsync(regionDomainModel);
            //dbContext.SaveChanges();
            ///await dbContext.SaveChangesAsync();
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
            // Map domain model back to dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                ReigionImageUrl = regionDomainModel.ReigionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new {id= regionDomainModel.Id}, regionDto);
        }

        //update
        [HttpPut]
        [Route("{id:Guid}")]
        //public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //map dto to domain model
            var regionDomainModel = new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                ReigionImageUrl = updateRegionRequestDto.ReigionImageUrl
            };
            //check if region existe
            //var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            ///var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id); 
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //map dto to domain model
            ///regionDomainModel.Code = updateRegionRequestDto.Code;
            ///regionDomainModel.Name = updateRegionRequestDto.Name;
            ///regionDomainModel.ReigionImageUrl = updateRegionRequestDto.ReigionImageUrl;
            //dbContext.SaveChanges();
            ///await dbContext.SaveChangesAsync();
            //convert domain model to dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                ReigionImageUrl= regionDomainModel.ReigionImageUrl
            };

            return Ok(regionDto); 
        }
        //delete
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            ///var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
           var  regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //deleye region
            ///dbContext.Regions.Remove(regionDomainModel);
            ///await dbContext.SaveChangesAsync();
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                ReigionImageUrl = regionDomainModel.ReigionImageUrl
            };
            return Ok(regionDto);
        }


    }
}
