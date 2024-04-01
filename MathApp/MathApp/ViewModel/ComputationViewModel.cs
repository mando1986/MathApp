using System.Collections.ObjectModel;
using System.Windows.Input;
using MathApp.Services;


namespace MathApp.ViewModel;

public partial class ComputationViewModel : BaseViewModel
{

    public ObservableCollection<Computation> Computations { get; } = new();

    MathAppService mathAppService;


    public ComputationViewModel(MathAppService mathAppService)
    {
        Title = "Math App";
        this.mathAppService = mathAppService;

    }

    bool isRefreshing;

    public bool IsRefreshing
    {
        get { return isRefreshing; }
        set
        {
            isRefreshing = value;
            OnPropertyChanged();
        }
    }


    public ICommand GetComputationCommand => new Command(async () => await GetComputationAsync());



    async Task GetComputationAsync()
    {
        if (IsRefreshing)

            try
            {
                var computations = await mathAppService.GetComputation();

                Computations.Clear();

                foreach (var computation in computations)
                    Computations.Add(computation);

                IsRefreshing = false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to retrieve questions: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
    }

    [RelayCommand]
    async Task GoToQuestion(Computation computation)
    {
        if (computation == null)
            return;

        await Shell.Current.GoToAsync(nameof(QuestionPage), true, new Dictionary<string, object>
        {
            {"Computation",computation}
        });
    }

}
