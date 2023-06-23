using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using static API.Services.BooksService;

namespace API.Controllers
{
    [ApiController]
    [Route("Books")]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _bookService;

        private APIMessage<Book> _ret;
        public BooksController(BooksService bookService)
        {
            _bookService = bookService;
            _ret = new APIMessage<Book>();
        }
        [HttpGet("BooksByProperty")]
        public ActionResult BooksByProperty(string value, BookProperty prop, DefaultService<Book>.OrderBy orderBy)
        {
            _bookService.BooksByProperty(value, prop, orderBy);
            if (_bookService.Data.Count == 0) return _ret.NotFound(_bookService);
            return _ret.Ok(_bookService);
        }
        [HttpGet("{id}/GetDeliveryTax")]
        public ActionResult GetDeliveryTax(int id)
        {
            _bookService.BookById(id);
            Dictionary<string, double> data = new Dictionary<string, double>();
            if (_bookService.SingleData == null) return _ret.NotFound(_bookService);
            if (_bookService.SingleData is Book)
            {
                var book = (Book)_bookService.SingleData;
                double tax = book.price * 0.2; // Calcula o valor do frete (20% do preço)
                data.Add("tax", tax);
            }
            return _ret.Ok(_bookService, data);
        }
    }
}