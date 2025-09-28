public class Comment
{
    // Attributes
    public string Author { get; set; }
    public string Text { get; set; }

    // Constructor
    public Comment(string author, string text)
    {
        Author = author;
        Text = text;
    }

    // Method to display a comment
    public void DisplayComment()
    {
        Console.WriteLine($"   {Author}: {Text}");
    }
}
