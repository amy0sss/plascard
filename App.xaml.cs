﻿using plascard.MVVM.Views;

namespace plascard
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new QnAView();
        }
    }
}
