using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudProjectController : ControllerBase
    {
        private readonly DataContext _context;

        public CrudProjectController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<List<CrudProject>>> Get()
        {
            return Ok(await _context.CrudProjects.ToListAsync());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<CrudProject>> Get(int id)
        {
            var student = await _context.CrudProjects.FindAsync(id);
            if (student == null)
                return BadRequest("Pupil not found");
            return Ok(student);
        }

        [HttpPost]

        public async Task<ActionResult<List<CrudProject>>> AddStudent([FromBody] CrudProjectDto student)
        {
            CrudProject studentData = new CrudProject
            {
                FirstName = student.FirstName,
                LastName = student.LastName,
                Age = Convert.ToInt32(student.Age),
                Subject = student.Subject
            };

            _context.CrudProjects.Add(studentData);
            await _context.SaveChangesAsync();

            return Ok(await _context.CrudProjects.ToListAsync());
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<List<CrudProject>>> UpdateStudent(CrudProject request)
        
        {
            Console.WriteLine(request);
            var dbPupil = await _context.CrudProjects.FindAsync(request.Id);
            if (dbPupil == null)
            return BadRequest("Pupil not found");

            dbPupil.FirstName = request.FirstName;
            dbPupil.LastName = request.LastName;
            dbPupil.Age = request.Age;
            dbPupil.Subject = request.Subject;

            await _context.SaveChangesAsync();
            
            return Ok(await _context.CrudProjects.ToListAsync());
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<List<CrudProject>>> Delete(int id)
        {
            var dbPupil = await _context.CrudProjects.FindAsync(id);
            if (dbPupil == null)
                return BadRequest("Pupil not found");

            _context.CrudProjects.Remove(dbPupil);
            await _context.SaveChangesAsync();

            return Ok(await _context.CrudProjects.ToListAsync());
        }
    }
}
