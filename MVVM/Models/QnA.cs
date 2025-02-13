namespace plascard.MVVM.Models
{
    // Model representing a flashcard with a question, answer, and an optional image.
    public class QnA
    {
        // The question text.
        public string Question { get; set; }
        // The answer text.
        public string Answer { get; set; }
        // The image path for the flashcard.
        public string ImagePath { get; set; }

        // Constructor to initialize the flashcard.
        public QnA(string question, string answer, string imagePath)
        {
            Question = question;
            Answer = answer;
            ImagePath = imagePath;
        }
    }
}