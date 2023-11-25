namespace OnlineLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime AddDate { get; set; }
        public string Text { get; set; }

        public Book(int id, string title, string author, DateTime addDate, string text)
        {
            Id = id;
            Title = title;
            Author = author;
            AddDate = addDate;
            Text = text;
        }
    }
}
