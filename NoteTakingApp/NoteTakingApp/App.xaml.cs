using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace NoteTakingApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    // Tämä korvaa vanhan MainPage = new AppShell();
    protected override Window CreateWindow(IActivationState? activationState)
    {
        // Luo ikkuna, jonka juurena on AppShell
        return new Window(new AppShell());
    }
}
