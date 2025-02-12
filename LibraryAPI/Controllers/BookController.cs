using AutoMapper;
using FluentValidation;
using Library.Data.Models;
using Library.Data.Repositories.Interfaces;
using LibraryAPI.Dtos.Book;
using LibraryAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookDto> _createValidation;
        private readonly IValidator<BookDto> _updateValidation;

        public BookController(IBookRepository bookRepo,
            IMapper mapper,
            IValidator<CreateBookDto> createValidation,
            IValidator<BookDto> updateValidation)
        {
            _bookRepo = bookRepo;
            _mapper = mapper;
            _createValidation = createValidation;
            _updateValidation = updateValidation;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> Get()
        {
            var books = await _bookRepo.GetAll();
            var bookDtos = _mapper.Map<IEnumerable<BookDto>>(books);
            return Ok(bookDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDto>> Get(int id)
        {
            try
            {
                var book = await _bookRepo.GetById(id);
                var bookDto = _mapper.Map<BookDto>(book);
                return Ok(bookDto);
            }catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateBookDto dto)
        {
            var validationResult = await _createValidation.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.GetErrorMessages());
            }

            try
            {
                var bookToCreate = _mapper.Map<Book>(dto);
                await _bookRepo.Add(bookToCreate);
                return Created();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<BookDto>> Update(BookDto dto)
        {
            var validationResult = await _updateValidation.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.GetErrorMessages());
            }

            try
            {
                var bookToUpdate = _mapper.Map<Book>(dto);
                await _bookRepo.Update(bookToUpdate);
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
                await _bookRepo.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
