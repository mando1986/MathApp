//namespace MathApp.View;

using MathApp.Services;

namespace MathApp;

public partial class QuestionPage : ContentPage
{
    public QuestionPage(QuestionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

}