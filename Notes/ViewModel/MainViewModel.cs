using DataBase.Abstractions;
using DataBase.Models;
using DevExpress.Mvvm;
using System.Collections.ObjectModel;

namespace Notes.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private readonly INotesDataAccess _notesDb;

        public Note CurrentNote
        {
            get => GetProperty(() => CurrentNote);
            set => SetProperty(() => CurrentNote, value);
        }

        public ObservableCollection<Note> NotesList { get; set; }

        public DelegateCommand AddCommand { get; set; }

        public DelegateCommand RemoveCommand { get; set; }

        public DelegateCommand SaveCommand { get; set; }

        public MainViewModel(INotesDataAccess notesDataAccess)
        {
            _notesDb = notesDataAccess;

            NotesList = new ObservableCollection<Note>(_notesDb.GetAllNotes());

            CurrentNote = NotesList[0];

            AddCommand = new DelegateCommand(AddNote);

            RemoveCommand = new DelegateCommand(RemoveNote);

            SaveCommand = new DelegateCommand(SaveNote);
        }

        private void AddNote()
        {
            var tmpNote = _notesDb.AddNote();
            NotesList.Add(tmpNote);
            CurrentNote = tmpNote;
        }

        private void RemoveNote()
        {
            _notesDb.RemoveNote(CurrentNote.Id);
            NotesList.Remove(CurrentNote);

            if (NotesList.Count != 0)
            {
                CurrentNote = NotesList[0];
            }
            else
            { 
                AddNote(); 
            }
        }

        private void SaveNote()
        {
            _notesDb.EditNote(CurrentNote);
            NotesList[NotesList.IndexOf(CurrentNote)] = CurrentNote;
            CurrentNote = NotesList[0];
        }
    }
}
