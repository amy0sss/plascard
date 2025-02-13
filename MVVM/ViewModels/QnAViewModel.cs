using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Maui;           // For Colors
using Microsoft.Maui.Controls;  // For Command
using plascard.MVVM.Models;

namespace plascard.MVVM.ViewModels
{
    // ViewModel for handling flashcards and UI logic.
    public class QnAViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Collection of flashcards.
        public ObservableCollection<QnA> Flashcards { get; set; }

        private QnA _currentFlashcard;
        // The currently displayed flashcard.
        public QnA CurrentFlashcard
        {
            get => _currentFlashcard;
            set
            {
                _currentFlashcard = value;
                OnPropertyChanged();                       // Notify change.
                OnPropertyChanged(nameof(CardText));       // Update displayed text.
                OnPropertyChanged(nameof(HasImage));         // Update image visibility.
            }
        }

        private bool _isAnswerVisible;
        // Flag indicating whether the answer is shown.
        public bool IsAnswerVisible
        {
            get => _isAnswerVisible;
            set
            {
                _isAnswerVisible = value;
                OnPropertyChanged();                       // Notify change.
                OnPropertyChanged(nameof(CardText));       // Update displayed text.
                OnPropertyChanged(nameof(CardBackgroundColor)); // Update background color.
            }
        }

        // Computed property: displays the answer if visible, otherwise the question.
        public string CardText => IsAnswerVisible ? CurrentFlashcard?.Answer : CurrentFlashcard?.Question;

        // Changes background color: light green for answer, white for question.
        public Color CardBackgroundColor => IsAnswerVisible ? Colors.LightGreen : Colors.White;

        // Indicates if the flashcard has an image.
        public bool HasImage => !string.IsNullOrEmpty(CurrentFlashcard?.ImagePath);

        // Navigation commands.
        public Command NextFlashcardCommand { get; set; }
        public Command PreviousFlashcardCommand { get; set; }
        public Command RandomizeCommand { get; set; }

        private Random _random;

        // Constructor to initialize flashcards and commands.
        public QnAViewModel()
        {
            // Initialize the flashcards collection.
            Flashcards = new ObservableCollection<QnA>()
            {
                new QnA("What is the capital of France?", "Paris", "france.jpg"),
                new QnA("Largest planet?", "Jupiter", "jupiter.jpg"),
                new QnA("Smallest country?", "Vatican City", "vatican.jpg"),
                new QnA("1 + 1 = ?", "2", "two.jpg"),
                new QnA("Who is the founder of ACT?", "BEBOT", "bebot.jpg")
            };

            _random = new Random();
            // Set the first flashcard as the current one.
            CurrentFlashcard = Flashcards.FirstOrDefault();
            IsAnswerVisible = false; // Start with the question showing.

            // Command to navigate to the next flashcard.
            NextFlashcardCommand = new Command(() =>
            {
                int currentIndex = Flashcards.IndexOf(CurrentFlashcard);
                int nextIndex = (currentIndex + 1) % Flashcards.Count;
                CurrentFlashcard = Flashcards[nextIndex];
                IsAnswerVisible = false; // Always show question initially.
            });

            // Command to navigate to the previous flashcard.
            PreviousFlashcardCommand = new Command(() =>
            {
                int currentIndex = Flashcards.IndexOf(CurrentFlashcard);
                int previousIndex = (currentIndex - 1 + Flashcards.Count) % Flashcards.Count;
                CurrentFlashcard = Flashcards[previousIndex];
                IsAnswerVisible = false;
            });

            // Command to randomize (shuffle) the flashcards.
            RandomizeCommand = new Command(() =>
            {
                var shuffled = Flashcards.OrderBy(x => _random.Next()).ToList();
                Flashcards.Clear();
                foreach (var card in shuffled)
                {
                    Flashcards.Add(card);
                }
                CurrentFlashcard = Flashcards.FirstOrDefault();
                IsAnswerVisible = false;
            });
        }

        // Method to notify the UI about property changes.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
