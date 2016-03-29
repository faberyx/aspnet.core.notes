using System;
using System.Linq;
using notes_manager.Models.Context;
using notes_manager.Models.Entities;

namespace notes_manager.Models.Repositories
{

    public class NotesRepository : IRepository<Note>,IDisposable
    {
        // Database Context
        private SQLiteContext db;

        /// <summary>
        /// Constructor used to insantiate the DBContext
        /// </summary>
        public NotesRepository()
        {
            this.db = new SQLiteContext();
        }

        /// <summary>
        /// Constructor uesed for testing DbContext passing it as a parameter
        /// </summary>
        /// <param name="context">SQLite context to pass</param>
        public NotesRepository(SQLiteContext context)
        {
            this.db = context;
        }


        /// <summary>
        /// Retrieve paginated list of notes
        /// </summary>
        /// <param name="pagingData">
        /// Contains all inofrmation about pagination, sort and result data
        /// SortBy: name of the field to sort
        /// SortDirection: sort direction  -> true:ASC  false:DESC
        /// PageNumber: current page numebr
        /// PageSize: total rows for a page
        /// Results: list of paginated notes
        /// </param>
        /// <returns>Resultpage object with a list of notes and pagination information</returns>
        public ResultPage<Note> GetPage(ResultPage<Note> pagingData)
        {
            //calculate the pagination data
            var skipCount = (pagingData.PageNumber - 1) * pagingData.PageSize;

            //retrieve the notes
            var notes = db.Notes.OrderBy(u=>u.Date).Skip(skipCount).Take(pagingData.PageSize);
                        //pagingData.SortBy + (pagingData.SortDirection ? " ASC" : " DESC")

            return new ResultPage<Note>
            {
                PageSize = pagingData.PageSize,
                TotalCount = db.Notes.Count(),
                PageNumber = pagingData.PageNumber,
                Results = notes
            };
        }
        
        /// <summary>
        /// Create a new note
        /// </summary>
        /// <param name="insertData">
        /// Note object to insert
        /// </param>
        public void Add(Note insertData)
        {

            insertData.Date = DateTime.Now;
            db.Notes.Add(insertData);
            db.SaveChanges();
        }
        
        /// <summary>
        /// Edit an existing note
        /// </summary>
        /// <param name="updateData">
        /// Note object to update
        /// </param>
        public void Edit(Note updateData)
        {

            insertData.Date = DateTime.Now;
            db.Notes.Add(insertData);
            db.SaveChanges();
        }

        /// <summary>
        /// Get a note by ID
        /// </summary>
        /// <param name="id">
        /// Id of the Note  to retrieve
        /// </param>

        public Note GetById(int id)
        { 
            return db.Notes.FirstOrDefault(x=>x.Id.Equals(id));
        }

        /// <summary>
        /// Delete an existing note
        /// </summary>
        /// <param name="insertData">
        /// Note object to delete
        /// </param>
        public void Delete(Note note)
        {
            db.Notes.Remove(note);
            db.SaveChanges();
        }

        /// <summary>
        /// Dispose of the database context
        /// </summary>
        /// <param name="disposing"></param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
