using NoteTakingApp.Models;
using NoteTakingApp.Services;

namespace NoteTakingApp;

public partial class MainPage : ContentPage
{
    List<Note> _notes = new();

    public MainPage()
    {
        InitializeComponent();
        LoadNotes();
    }

    void LoadNotes()
    {
        _notes = NoteStorage.GetAll().ToList();
        NotesView.ItemsSource = _notes;
    }

    async void OnAddClicked(object sender, EventArgs e)
    {
        var note = NoteStorage.Create();
        await Shell.Current.GoToAsync(nameof(Pages.NotePage),
            new Dictionary<string, object?> { ["Note"] = note });
    }

    void OnRefreshClicked(object sender, EventArgs e) => LoadNotes();

    async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Note note)
        {
            ((CollectionView)sender).SelectedItem = null; // clear selection
            await Shell.Current.GoToAsync(nameof(Pages.NotePage),
                new Dictionary<string, object?> { ["Note"] = note });
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadNotes(); // refresh after returning from editor
    }
}
