using System.ComponentModel;

namespace Cource.ViewModels
{
    public class NewsModel
    {
        public int Id { get; set; }

        [DisplayName("Заголовок")]
        public string Title { get; set; }

        [DisplayName("Текст")]
        public string Text { get; set; }
    }
}
