using DataBase.Models;

namespace DataBase.Abstractions
{
    public interface INotesDataAccess
    {
        int MaxNoteId { get; }

        void CreateIfNotExistsDataBase();

        Note AddNote();

        void EditNote(Note note);

        void RemoveNote(int id);

        void DeleteDataBase();

        List<Note> GetAllNotes();
    }
}
