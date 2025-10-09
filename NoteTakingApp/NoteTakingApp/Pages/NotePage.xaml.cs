using NoteTakingApp.Models;
using NoteTakingApp.Services;

namespace NoteTakingApp.Pages;

[QueryProperty(nameof(Note), "Note")]
public partial class NotePage : ContentPage
{
    private Note? _note;
    public Note? Note
    {
        get => _note;
        set { _note = value; BindingContext = _note; }
    }

    public NotePage() => InitializeComponent();

    async void OnSave(object sender, EventArgs e)
    {
        if (Note is null) return;
        NoteStorage.Save(Note);
        await DisplayAlert("Saved", "Note saved.", "OK");
        await Shell.Current.GoToAsync("..");
    }

    async void OnDelete(object sender, EventArgs e)
    {
        if (Note is null) return;
        if (!await DisplayAlert("Delete", "Delete this note?", "Yes", "No")) return;

        NoteStorage.Delete(Note);
        await DisplayAlert("Deleted", "Note deleted.", "OK");
        await Shell.Current.GoToAsync("..");
    }

    async void OnShare(object sender, EventArgs e)
    {
        if (Note is null) return;
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Title = "Share note",
            Text = Note.Text
        });
    }
}
