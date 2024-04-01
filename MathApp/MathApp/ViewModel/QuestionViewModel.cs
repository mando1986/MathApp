namespace MathApp.ViewModel;

[QueryProperty(nameof(Computation), "Computation")]

public partial class QuestionViewModel : BaseViewModel
{
    public QuestionViewModel()
    {
    }

    [ObservableProperty]
    Computation computation;
}
