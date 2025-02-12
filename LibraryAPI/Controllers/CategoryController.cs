using AutoMapper;
using FluentValidation;
using Library.Data.Models;
using Library.Data.Repositories.Interfaces;
using LibraryAPI.Dtos.Category;
using LibraryAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _createValidation;
        private readonly IValidator<CategoryDto> _updateValidation;

        public CategoryController(ICategoryRepository categoryRepo,
            IMapper mapper,
            IValidator<CreateCategoryDto> createValidation,
            IValidator<CategoryDto> updateValidation)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _createValidation = createValidation;
            _updateValidation = updateValidation;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            var categorys = await _categoryRepo.GetAll();
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDto>>(categorys);
            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> Get(int id)
        {
            try
            {
                var category = await _categoryRepo.GetById(id);
                var categoryDto = _mapper.Map<CategoryDto>(category);
                return Ok(categoryDto);
            }catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateCategoryDto dto)
        {
            var validationResult = await _createValidation.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.GetErrorMessages());
            }

            try
            {
                var categoryToCreate = _mapper.Map<Category>(dto);
                await _categoryRepo.Add(categoryToCreate);
                return Created();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDto>> Update(CategoryDto dto)
        {
            var validationResult = await _updateValidation.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.GetErrorMessages());
            }

            try
            {
                var categoryToUpdate = _mapper.Map<Category>(dto);
                await _categoryRepo.Update(categoryToUpdate);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryRepo.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
