using AutoMapper;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Net;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly IUsersBookRepository _usersBookRepository;
        private readonly IPhotoService _photoService;
        private readonly UserManager<AppUser> _userManager;

        public BookController(ILogger<BookController> logger, IMapper mapper, IBookRepository bookRepository, IUsersBookRepository usersBookRepository, IPhotoService photoService, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _bookRepository = bookRepository;
            _usersBookRepository = usersBookRepository;
            _photoService = photoService;
            _userManager = userManager;
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

            IEnumerable<int> addedBooks = Array.Empty<int>();
            if (User.Identity.IsAuthenticated)
            {
                var userId = _userManager.GetUserId(User);
                addedBooks = await _usersBookRepository.GetAllBooksByUserId(userId);
            }

            var pager = new Pager(totalBooks, pageIndex, pageSize);

            var booksViewModel = new BooksViewModel()
            {
                Books = booksMainInfo,
                AddedBooks = addedBooks,
                SearchString = searchString,
                Pager = pager
            };

            return View(booksViewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetById(id);
            var bookAdded = await _usersBookRepository.UserHasABook(_userManager.GetUserId(User), id);
            var bookDetailsViewModel = new BookDetailsViewModel()
            {
                Book = book,
                CanBeAdded = !bookAdded,
            };

            return View(bookDetailsViewModel);
        }

        public async Task AddBookToUsersBook(int bookId)
        {
            var usersBook = new UsersBook()
            {
                Status = Data.Enum.UsersBookStatus.InWishlist,
                AddingDate = DateTime.UtcNow,
                UserId = _userManager.GetUserId(User),
                BookId = bookId
            };

            await _usersBookRepository.Add(usersBook);
        }

        public async Task<IActionResult> AddBookToUsersBookFromDetails(int bookId)
        {
            await AddBookToUsersBook(bookId);

            return RedirectToAction("Details", new { Id = bookId });
        }

        public async Task<IActionResult> AddBookToUsersBookFromIndex(int bookId, string searchString, int pageIndex = 1)
        {
            await AddBookToUsersBook(bookId);

            return RedirectToAction("Index", new { searchString = searchString, pageIndex = pageIndex });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateBookViewModel bookVM)
        {
            var book = _mapper.Map<Book>(bookVM);
            book.Image = (await _photoService.AddPhotoAsync(bookVM.Image!)).Url.ToString();

            await _bookRepository.Add(book);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int bookId)
        {
            var book = await _bookRepository.GetById(bookId);
            if (book == null)
                return View("Error");

            var bookVM = _mapper.Map<EditBookViewModel>(book);

            return View(bookVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditBookViewModel bookVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit book");
                return View("Edit", bookVM);
            }

            var book = await _bookRepository.GetByIdNoTracking(bookVM.Id);

            if (book == null)
            {
                return View("Error");
            }

            Book editedBook = null;

            if (bookVM.ImageFile == null)
            {
                editedBook = _mapper.Map<Book>(bookVM);
                editedBook.CreatedDate = book.CreatedDate;
                editedBook.CreatedById = book.CreatedById;
                //last modified by
            }
            else
            {
                var photoResult = await _photoService.AddPhotoAsync(bookVM.ImageFile);

                if (photoResult.Error != null)
                {
                    ModelState.AddModelError("Image", "Photo upload failed");
                    return View(bookVM);
                }

                if (!string.IsNullOrEmpty(book.Image))
                {
                    _ = _photoService.DeletePhotoAsync(book.Image);
                }

                editedBook = _mapper.Map<Book>(bookVM);
                editedBook.Image = photoResult.Url.ToString();
                editedBook.CreatedDate = book.CreatedDate;
                editedBook.CreatedById = book.CreatedById;
                //last modified by
            }
            await _bookRepository.Update(editedBook);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int bookId)
        {
            var book = await _bookRepository.GetByIdNoTracking(bookId);

            if (book == null)
            {
                return View("Error");
            }

            if (!string.IsNullOrEmpty(book.Image) && book.Image.Contains("cloudinary"))
            {
                _ = _photoService.DeletePhotoAsync(book.Image);
            }

            await _usersBookRepository.DeleteAllUsersBooksWithSessionsByBookId(bookId);
            await _bookRepository.Delete(book);

            return RedirectToAction("Index");
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
