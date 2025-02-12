using AutoMapper;
using FluentValidation;
using Library.Data.Models;
using Library.Data.Repositories.Interfaces;
using LibraryAPI.Dtos.Author;
using LibraryAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateAuthorDto> _createValidation;
        private readonly IValidator<AuthorDto> _updateValidation;

        public AuthorController(IAuthorRepository authorRepo,
            IMapper mapper,
            IValidator<CreateAuthorDto> createValidation,
            IValidator<AuthorDto> updateValidation)
        {
            _authorRepo = authorRepo;
            _mapper = mapper;
            _createValidation = createValidation;
            _updateValidation = updateValidation;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> Get()
        {
            var authors = await _authorRepo.GetAll();
            var authorDtos = _mapper.Map<IEnumerable<AuthorDto>>(authors);
            return Ok(authorDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> Get(int id)
        {
            try
            {
                var author = await _authorRepo.GetById(id);
                var authorDto = _mapper.Map<AuthorDto>(author);
                return Ok(authorDto);
            }catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateAuthorDto dto)
        {
            var validationResult = await _createValidation.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.GetErrorMessages());
            }

            try
            {
                var authorToCreate = _mapper.Map<Author>(dto);
                await _authorRepo.Add(authorToCreate);
                return Created();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<AuthorDto>> Update(AuthorDto dto)
        {
            var validationResult = await _updateValidation.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.GetErrorMessages());
            }

            try
            {
                var authorToUpdate = _mapper.Map<Author>(dto);
                await _authorRepo.Update(authorToUpdate);
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
                await _authorRepo.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
