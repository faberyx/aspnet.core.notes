using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using notes_manager.Models;
using notes_manager.Models.Entities;
using notes_manager.Models.Repositories;

namespace notes_manager.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        
         private IRepository<Note> _repository;
         private ILogger _logger;

        /// <summary>
        /// Contructor injection of the repository trough Unity
        /// </summary>
        /// <param name="repository">The repository to inject</param>
        public NotesController(IRepository<Note> repository, ILogger<NotesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        /// <summary>
        /// GET Call to retrieve paginated and sorted notes
        /// </summary>
        /// <param name="pagingData">
        /// Object containg all information about pagination and sort
        /// </param>
        /// <returns>
        /// List of paginated notes and pagination and sort information
        /// </returns>
        [HttpGet]
        public IActionResult Get(ResultPage<Note> pagingData)
        {
              if (pagingData == null)
                return HttpBadRequest();

            return Ok(_repository.GetPage(pagingData));

        }
 

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Note note)
        {
            _logger.LogInformation(note.Description);
            if (note == null)
                return HttpNotFound(); 

            if (!ModelState.IsValid)
                return HttpBadRequest(ModelState);

            _repository.Add(note);
            
            return CreatedAtAction("Get",note);

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
