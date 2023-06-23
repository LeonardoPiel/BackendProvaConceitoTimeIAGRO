using API.Models;
using System.Text.Json;

namespace API.Services
{
    public class BooksService : DefaultService<Book>
    {
        public BooksService(IConfiguration configuration) : base(configuration)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            try
            {
                Data = JsonSerializer.Deserialize<List<Book>>(GetJsonContent("data.json"), options);
            }
            catch (Exception ex)
            {
                Errors.Add("Erro ao iniciar tratamento dos dados de livros => Mensagem" + ex);
            }
        }
        public void BookById(int id)
        {
            SingleData = Data.FirstOrDefault(e => e.id == id);
        }
        public void BooksByProperty(string value, BookProperty property, OrderBy order)
        {
            var filteredBooks = new List<Book>();
            try
            {
                var propertyInfo = typeof(Book).GetProperty(property.ToString());
                if (propertyInfo == null)
                {
                    propertyInfo = typeof(Specifications).GetProperty(property.ToString());
                    if (propertyInfo == null)
                    {
                        Data = filteredBooks;
                        return;
                    }
                }
                foreach (var data in Data)
                {
                    if (propertyInfo.ReflectedType == typeof(Book))
                    {
                        var propertyValue = propertyInfo.GetValue(data);
                        if (propertyValue != null && propertyValue.ToString().ToLower() == value.ToString().ToLower())
                            filteredBooks.Add(data);
                    }
                    if (propertyInfo.ReflectedType == typeof(Specifications))
                    {
                        var propertyValue = propertyInfo.GetValue(data.specifications);
                        if (propertyValue != null && propertyValue.ToString().ToLower().Contains(value.ToString().ToLower()))
                            filteredBooks.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                Errors.Add("Erro ao buscar registro. Mensagem => " + ex.Message);
            }
            Data = order == OrderBy.Desc ? filteredBooks.OrderByDescending(e => e.price).ToList() : filteredBooks.OrderBy(e => e.price).ToList();
        }
        public enum BookProperty
        {
            id,
            name,
            price,
            specifications,
            Originallypublished,
            Author,
            Pagecount,
            Illustrator,
            Genres
        }
    }
}