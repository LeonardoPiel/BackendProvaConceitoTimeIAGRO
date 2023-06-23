using System.ComponentModel;

namespace API.Services
{
    public class DefaultService <T>
    {
        private readonly IConfiguration _configuration;
        public List<string> Errors { get; set; }
        public List<T> Data { get; set; }
        public object SingleData { get; set; }
        public DefaultService(IConfiguration configuration)
        {
            _configuration = configuration;
            Errors = new List<string>();
        }
        public enum OrderBy
        {
            [Description("Descending")]
            Desc = 1,
            [Description("Ascending")]
            Asc = 2
        }
        public string GetJsonContent(string fileName)
        {
            try
            {
                var dataFolderPath = _configuration.GetValue<string>("DataFolderPath");
                var filePath = Path.Combine(dataFolderPath, fileName);

                if (File.Exists(filePath))
                {
                    return File.ReadAllText(filePath);
                }
            }
            catch (Exception ex)
            {
                Errors.Add("Ocorreu um erro ao ler o arquivo JSON. "+ ex);
            }
            return string.Empty;
        }
    }
}
