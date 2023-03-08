using DataBase.Abstractions;
using DataBase.Models;

namespace DataBase.Core
{
    public class NotesDataAccess : INotesDataAccess
    {
        public int MaxNoteId 
        { 
            get 
            { 
                using var context = new NotesDbContext();
                if (context.Notes.Count() == 0)
                {
                    return 0;
                }
                return context.Notes.Max(x => x.Id);
            } 
        }

        public Note AddNote()
        {
            using var context = new NotesDbContext();
            var tmp = new Note { Id = MaxNoteId + 1, Name = (MaxNoteId + 1).ToString(), Description = "Here you can write your ideas" } ;
            context.Notes.AddAsync(tmp);
            context.SaveChanges();
            return tmp;
        }

        public void CreateIfNotExistsDataBase()
        {
            using var context = new NotesDbContext();
            context.Database.EnsureCreated();

            if (MaxNoteId == 0)
            {
                AddNote();
            }
        }

        public void DeleteDataBase()
        {
            using var context = new NotesDbContext();
            context.Database.EnsureDeleted();
        }

        public void EditNote(Note note)
        {
            using var context = new NotesDbContext();
            try
            {
                context.Notes.Find(note.Id).Name = note.Name;
                context.Notes.Find(note.Id).Description = note.Description;
                context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //logging
                throw;
            }
        }

        public List<Note> GetAllNotes()
        {
            using var context = new NotesDbContext();
            return context.Notes.ToList();
        }

        public void RemoveNote(int id)
        {
            using var context = new NotesDbContext();

            try
            {
                context.Notes.Remove(context.Notes.Find(id));
                context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //logging
                throw;
            }
        }
    }
}
