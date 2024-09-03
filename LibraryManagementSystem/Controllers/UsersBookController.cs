using AutoMapper;
using LibraryManagementSystem.Data.Enum;
using LibraryManagementSystem.DTOs.ReadingSession;
using LibraryManagementSystem.DTOs.UsersBook;
using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Net;
using static System.Reflection.Metadata.BlobBuilder;

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

        public async Task<IActionResult> Details(int usersBookId)
        {
            var book = await _usersBookRepository.GetUsersBookById(usersBookId);
            var usersBook = _mapper.Map<UsersBookInfoDto>(book);

            var readingSessions = await _readingSessionRepository.GetAllByUsersBookId(usersBook.Id);
            var readingSessionsInfo = _mapper.Map<IEnumerable<ReadingSessionInfoDto>>(readingSessions);

            var usersBookWithSessionViewModel = new UsersBookWithSessionViewModel
            {
                UsersBook = usersBook,
                ReadingSessions = readingSessionsInfo
            };

            return View(usersBookWithSessionViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Details(UsersBookWithSessionViewModel model)
        {
            var usersBook = await _usersBookRepository.GetById(model.UsersBook.Id);

            model.UsersBook.LendTo = model.UsersBook.LendTo ?? "";

            if (model.UsersBook.LendTo != usersBook.LendTo || model.UsersBook.Status != usersBook.Status)
            {
                usersBook.LendTo = model.UsersBook.LendTo;
                usersBook.Status = model.UsersBook.Status;

                await _usersBookRepository.Update(usersBook);
            }

            return RedirectToAction("Details", "UsersBook", new { usersBookId = model.UsersBook.Id });
        }

        public async Task<IActionResult> DeleteUsersBook(int usersBookId)
        {
            await _usersBookRepository.DeleteUsersBookWithSessions(usersBookId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<JsonResult> AddReadingSession(int startPage, int endPage, int usersBookId)
        {
            if (startPage > 0 && endPage >= startPage)
            {
                var existingPage = await _readingSessionRepository.AlreadyReadPages(startPage, endPage, usersBookId);

                if (!existingPage)
                {
                    var readingSession = new ReadingSession()
                    {
                        StartPage = startPage,
                        EndPage = endPage,
                        UsersBookId = usersBookId
                    };

                    await _readingSessionRepository.Add(readingSession);

                    return Json(new { success = true, message = "Reading pages added successfully." });
                }
                return Json(new { success = false, message = "These pages have already been read." });
            }
            return Json(new { success = false, message = "Start page must be greater than 0 and less than end page." });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteReadingSession(int usersBookId)
        {
            await _readingSessionRepository.DeleteLastSession(usersBookId);

            return Json(new { success = true, message = "These pages have already been read." });
        }

        public async Task<IActionResult> GetUpdatedReadingSessions(int usersBookId)
        {
            var readingSessions = await _readingSessionRepository.GetAllByUsersBookId(usersBookId);
            var readingSessionsInfo = _mapper.Map<IEnumerable<ReadingSessionInfoDto>>(readingSessions);

            return PartialView("_ReadingSessionsPartial", readingSessionsInfo);
        }

    }
}
