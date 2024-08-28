using AutoMapper;
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
        private readonly IMapper _mapper;

        public UsersBookController(IUsersBookRepository usersBookRepository, IBookRepository bookRepository, IMapper mapper)
        {
            _usersBookRepository = usersBookRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
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
    }
}
