
namespace MathApp.View;

public partial class MainPage : ContentPage
{

    public MainPage(ComputationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

}
