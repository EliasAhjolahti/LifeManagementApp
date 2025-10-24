using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NoteTakingApp.Models;
using NoteTakingApp.Services;

namespace NoteTakingApp.ViewModels;

[QueryProperty(nameof(Note), "Note")]
public partial class NoteViewModel : ObservableObject
{
    [ObservableProperty] private string title = "Edit Note";
    [ObservableProperty] private string text = string.Empty;
    [ObservableProperty] private string fileName = string.Empty;
    [ObservableProperty] private DateTime date;

    private Note? note;
    public Note? Note
    {
        get => note;
        set
        {
            note = value;
            if (note is null) return;
            Text = note.Text;
            FileName = note.FileName;
            Date = note.Date;
        }
    }

    [RelayCommand]
    private async Task Save()
    {
        if (Note is null) return;
        Note.Text = Text;
        Note.FileName = FileName;
        Note.Date = DateTime.Now;
        NoteStorage.Save(Note);
        await Shell.Current.DisplayAlert("Saved", "Note saved.", "OK");
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task Delete()
    {
        if (Note is null) return;
        var confirm = await Shell.Current.DisplayAlert("Delete", "Delete this note?", "Yes", "No");
        if (!confirm) return;
        NoteStorage.Delete(Note);
        await Shell.Current.DisplayAlert("Deleted", "Note deleted.", "OK");
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task ShareNote()
    {
        if (Note is null) return;
        await Share.Default.RequestAsync(new ShareTextRequest
        {
            Title = "Share note",
            Text = Text
        });
    }
}
