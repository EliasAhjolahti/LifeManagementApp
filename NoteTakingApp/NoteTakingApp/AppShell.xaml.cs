namespace NoteTakingApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        // Route to the editor page
        Routing.RegisterRoute(nameof(Pages.NotePage), typeof(Pages.NotePage));
    }
}
