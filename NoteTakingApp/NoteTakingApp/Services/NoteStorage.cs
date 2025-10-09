using NoteTakingApp.Models;

namespace NoteTakingApp.Services;

public static class NoteStorage
{
    static string NotesDir => FileSystem.AppDataDirectory;

    public static IEnumerable<Note> GetAll()
    {
        Directory.CreateDirectory(NotesDir);

        return Directory.EnumerateFiles(NotesDir, "*.notes.txt")
            .Select(path => new FileInfo(path))
            .OrderByDescending(fi => fi.LastWriteTimeUtc)
            .Select(fi => new Note
            {
                FileName = fi.FullName,
                Text = File.ReadAllText(fi.FullName),
                Date = fi.LastWriteTime
            });
    }

    public static Note Create()
    {
        Directory.CreateDirectory(NotesDir);
        var path = Path.Combine(NotesDir, $"{Path.GetRandomFileName()}.notes.txt");
        File.WriteAllText(path, string.Empty);
        return new Note { FileName = path, Text = string.Empty, Date = DateTime.Now };
    }

    public static void Save(Note note)
    {
        Directory.CreateDirectory(NotesDir);
        File.WriteAllText(note.FileName, note.Text);
        File.SetLastWriteTime(note.FileName, DateTime.Now);
        note.Date = File.GetLastWriteTime(note.FileName);
    }

    public static void Delete(Note note)
    {
        if (File.Exists(note.FileName))
            File.Delete(note.FileName);
    }
}
