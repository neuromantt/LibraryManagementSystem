using AutoMapper;
using LibraryManagementSystem.Data.Enum;
using LibraryManagementSystem.DTOs.ReadingSession;
using LibraryManagementSystem.DTOs.UsersBook;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LibraryManagementSystem.Controllers
{
    public class UsersBookController : Controller
    {
        private readonly IUsersBookRepository _usersBookRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IReadingSessionRepository _readingSessionRepository;
        private readonly IMapper _mapper;

        public UsersBookController(IUsersBookRepository usersBookRepository, IBookRepository bookRepository, IMapper mapper, IReadingSessionRepository readingSessionRepository)
        {
            _usersBookRepository = usersBookRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
            _readingSessionRepository = readingSessionRepository;
        }

        public async Task<IActionResult> Index(string searchString, int pageIndex = 1)
        {
            IEnumerable<UsersBook> books = null;
            const int pageSize = 30;
            int totalBooks;

            if (searchString.IsNullOrEmpty())
            {
                
                books = await _usersBookRepository.GetUsersBooksFromPageWithSize("", pageIndex, pageSize);
                totalBooks = await _usersBookRepository.CountAllUsersBooks("");
            }
            else
            {
                books = await _usersBookRepository.GetUsersBooksByTitleOrAuthorFromPageWithSize("", searchString, pageIndex, pageSize);
                totalBooks = await _usersBookRepository.CountAllUsersBooksSearchByTitleOrAuthor("", searchString);
            }

            IEnumerable<UsersBookInfoDto> usersBooksInfo = _mapper.Map<IEnumerable<UsersBookInfoDto>>(books);

            var pager = new Pager(totalBooks, pageIndex, pageSize);

            var usersBooksViewModel = new UsersBooksViewModel()
            {
                Books = usersBooksInfo,
                SearchString = searchString,
                Pager = pager
            };

            return View(usersBooksViewModel);
        }

        public async Task<IActionResult> Details(int id, UsersBookStatus status, string lendTo, int? bookId, string bookTitle, string bookAuthor, string bookImage)
        {
            var usersBook = new UsersBookInfoDto
            {
                Id = id,
                Status = status,
                LendTo = lendTo,
                BookId = bookId,
                BookTitle = bookTitle,
                BookAuthor = bookAuthor,
                BookImage = bookImage
            };

            var readingSessions = await _readingSessionRepository.GetAllByUsersBookId(id);
            var readingSessionsInfo = _mapper.Map<IEnumerable<ReadingSessionInfoDto>>(readingSessions);

            var usersBookWithSessionViewModel = new UsersBookWithSessionViewModel
            {
                Book = usersBook,
                ReadingSessions = readingSessionsInfo
            };

            return View(usersBookWithSessionViewModel);
        }
    }
}
