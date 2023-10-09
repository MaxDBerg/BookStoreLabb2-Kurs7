using BookstoreApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MinimalAPI_Books.Models;
using MinimalAPI_Books.Models.DTO.AuthorDTO;
using MinimalAPI_Books.Models.DTO.BookDTO;
using MinimalAPI_Books.Models.DTO.GenreDTO;
using MinimalAPI_Books.Models.DTO.LanguageDTO;
using Newtonsoft.Json;
using System.Text;

namespace BookstoreApp.Controllers
{
    public class BookController : Controller
    {
        private readonly HttpClient _client;
        Uri baseAddress = new Uri("https://localhost:7082");

        public BookController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        // GET: BookController
        [HttpGet]
        public ActionResult Index()
        {
            List<BookReadDTO> bookList = new List<BookReadDTO>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "books").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                bookList = JsonConvert.DeserializeObject<List<BookReadDTO>>(data);
            }
            return View(bookList);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            BookReadDTOComplete book = new BookReadDTOComplete();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "books/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<BookReadDTOComplete>(data);
            }
            return View(book);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var genreList = GetSelectListFromApi<GenreReadDTO>("genres");
            var genreSelectList = genreList.Select(genre => new SelectListItem
            {
                Text = genre.Name,
                Value = genre.Id.ToString()
            }).ToList();

            var authorList = GetSelectListFromApi<AuthorReadDTO>("authors");
            var authorSelectList = authorList.Select(author => new SelectListItem
            {
                Text = author.Name,
                Value = author.Id.ToString()
            }).ToList();

            var languageList = GetSelectListFromApi<LanguageReadDTO>("languages");
            var languageSelectList = languageList.Select(language => new SelectListItem
            {
                Text = language.Name,
                Value = language.Id.ToString()
            }).ToList();

            ViewBag.AvailableGenres = genreSelectList;
            ViewBag.AvailableAuthors = authorSelectList;
            ViewBag.AvailableLanguages = languageSelectList;

            return View();
        }

        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookCreateDTO bookCreate)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(bookCreate);

                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "books", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(bookCreate);
        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {
            var genreList = GetSelectListFromApi<GenreReadDTO>("genres");
            var genreSelectList = genreList.Select(genre => new SelectListItem
            {
                Text = genre.Name,
                Value = genre.Id.ToString()
            }).ToList();

            var authorList = GetSelectListFromApi<AuthorReadDTO>("authors");
            var authorSelectList = authorList.Select(author => new SelectListItem
            {
                Text = author.Name,
                Value = author.Id.ToString()
            }).ToList();

            var languageList = GetSelectListFromApi<LanguageReadDTO>("languages");
            var languageSelectList = languageList.Select(language => new SelectListItem
            {
                Text = language.Name,
                Value = language.Id.ToString()
            }).ToList();

            ViewBag.AvailableGenres = genreSelectList;
            ViewBag.AvailableAuthors = authorSelectList;
            ViewBag.AvailableLanguages = languageSelectList;

            BookReadDTOComplete book = new BookReadDTOComplete();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "books/" + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                book = JsonConvert.DeserializeObject<BookReadDTOComplete>(data);

                var mappedBook = new BookUpdateDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    Description = book.Description,
                    LanguageId = book.Language.Id,
                    AuthorId = book.Author.Id,
                    GenreIds = book.Genres.Select(genre => genre.Id).ToList()
                };
                return View(mappedBook);
            }
            return View();
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookUpdateDTO bookUpdate)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(bookUpdate);

                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "books", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(bookUpdate);
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "books/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private List<T> GetSelectListFromApi<T>(string apiEndpoint)
        {
            var availableData = new List<T>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + apiEndpoint).Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                availableData = JsonConvert.DeserializeObject<List<T>>(data);
            }

            return availableData;
        }
    }
}
