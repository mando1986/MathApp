namespace MathApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(QuestionPage), typeof(QuestionPage));
    }
}

