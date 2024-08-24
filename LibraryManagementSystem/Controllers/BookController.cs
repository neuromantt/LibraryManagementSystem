using AutoMapper;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public BookController(ILogger<BookController> logger, ApplicationDBContext context, IMapper mapper, IBookRepository bookRepository)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<Book> books = null;

            if (searchString.IsNullOrEmpty())
            {
                books = await _bookRepository.GetAll();
            }
            else
            {
                books = await _bookRepository.SearchByTitleOrAuthor(searchString);
            }

            IEnumerable<BookMainInfoDto> booksMainInfo = _mapper.Map<IEnumerable<BookMainInfoDto>>(books);

            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
