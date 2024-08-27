using AutoMapper;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModel;
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

        public async Task<IActionResult> Index(string searchString, int pageIndex = 1)
        {
            IEnumerable<Book> books = null;
            const int pageSize = 30;
            int totalBooks;

            if (searchString.IsNullOrEmpty())
            {
                books = await _bookRepository.GetAllFromPageWithSize(pageIndex, pageSize);
                totalBooks = await _bookRepository.CountAll();
            }
            else
            {
                books = await _bookRepository.GetAllSearchByTitleOrAuthorFromPageWithSize(searchString, pageIndex, pageSize);
                totalBooks = await _bookRepository.CountAllSearchByTitleOrAuthor(searchString);
            }

            IEnumerable<BookMainInfoDto> booksMainInfo = _mapper.Map<IEnumerable<BookMainInfoDto>>(books);

            var pager = new Pager(totalBooks, pageIndex, pageSize);

            var booksViewModel = new ShowBooksViewModel()
            {
                Books = booksMainInfo,
                SearchString = searchString,
                Pager = pager
            };

            return View(booksViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetById(id);
            return View(book);
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
