using NoteTakingApp.ViewModels;

namespace NoteTakingApp.Pages;

public partial class NotePage : ContentPage
{
    public NotePage(NoteViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm; // QueryProperty täyttää vm.Note:n navigoinnista
    }
}
