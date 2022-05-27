using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; //ControllerBase (MVC controller without views)
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using TestApi.Data;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/[controller]")] //Map requests to action methods e.g. GET: /api/Job > [HtttpGet] public async Task<IEnumerable<Job>> Get()
    [ApiController] //Attribute provides controller level: Attribute routing requirement, Automatic HTTP 400 responses, Binding source parameter inference, Multipart/form-data request inference, Problem details for error status codes
    public class JobController : ControllerBase //Provides methods for handling HTTP requests
    {
        private readonly JobDbContext _context;

        public JobController(JobDbContext context) 
            => _context = context;

        //[ActionName("GetJobs")] Does not seem to be supported by swagger.
     
        
        //[HttpGet, Route("custom route if needed")]
        [HttpGet] //leveraging ApiController attribute for automatic responses. The manual response filter attributes are merely for enhacned documentation
        public async Task<IEnumerable<Job>> GetAll() 
            => await _context.Jobs.ToListAsync();

        [HttpGet("{id}")] //! I believe it also takes "id" in the HttpGet case..
        [ProducesResponseType(typeof(Job), StatusCodes.Status200OK)] //Filter provided by AspNetCore.Mvc, for OpenApi documentation.
        [ProducesResponseType(typeof(Job), StatusCodes.Status400BadRequest)] //OpenApi specification enhancement. Automatic HTTP 400 responses due to [ApiController] attribute on controller level.
        public async Task<IActionResult> GetById(int id)//IActionResult using namespace ..Mvc
        {
            var job = await _context.Jobs.FindAsync(id);
            return job == null? NotFound() : Ok(job); //Members methods of ControllerBase
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)] //Produce server respondse with Code 201. 
        public async Task<IActionResult> Create(Job job)
        {
            job.Created = DateTime.Now;
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();

            //Produces and returns a reponse header for fetching newly created object e.g: "location: https://localhost:7065/api/Job/id?id=2"
            return CreatedAtAction(
                    actionName: nameof(GetById), //[HttpGet("id")] GetById: acton method used to access created object. URN: "/api/Job/id" 
                    routeValues: new { id = job.Id }, //GetById(int id): id of the object to get. Route data for query string: "?id=2"
                    value: job); //[HttpPost] Create(object value): the created object itself i.e. content value to format in the entity body  
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, Job job)
        {
            if (id != job.Id) return BadRequest();
            _context.Entry(job).State = EntityState.Modified; //EF Core change tracking: entity in db is tracked by db context
            await _context.SaveChangesAsync();

            return NoContent();// Response indicates that the Server has successfully fulfilled the request, no content to send in the response payload body.
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var jobToDelete = await _context.Jobs.FindAsync(id);
            if (jobToDelete == null) return NotFound();

            _context.Jobs.Remove(jobToDelete);
            await _context.SaveChangesAsync();
            return NoContent();

        }




    }
}
