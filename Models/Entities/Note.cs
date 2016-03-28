using System;
using System.ComponentModel.DataAnnotations;

namespace notes_manager.Models.Entities
{
    /// <summary>
    /// Entity that represents the Note table on the SQLite Database
    /// Id: Autoincrement Primary Key
    /// Description: Description of the Note
    /// Date: Timestamp of when the Note is created
    /// </summary>
    public class Note
    {
        [Key]
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Description is required")]
        [StringLength(100, ErrorMessage = "Description cannot be longer than 100 characters.")]
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}