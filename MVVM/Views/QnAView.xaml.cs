using System;
using Microsoft.Maui.Controls;
using plascard.MVVM.ViewModels;

namespace plascard.MVVM.Views
{
    public partial class QnAView : ContentPage
    {
        public QnAView()
        {
            InitializeComponent();
            // Set binding context
            BindingContext = new QnAViewModel();
        }

        private async void OnCardTapped(object sender, EventArgs e)
        {
            // 'sender' is the CardFrame we named in XAML.
            var card = sender as VisualElement;
            if (card == null) return;

            // Rotate the card to 360
            await card.RotateYTo(0, 360, Easing.Linear);

            // Toggle the content by switching the view model state
            if (BindingContext is QnAViewModel viewModel)
            {
                viewModel.IsAnswerVisible = !viewModel.IsAnswerVisible;
            }

            // Rotate back to 0° to complete the flip animation
            await card.RotateYTo(360, 0, Easing.Linear);
        }
    }
}
