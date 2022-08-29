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
            List<CrudProject> studentList = new List<CrudProject>();
            var data = await _context.CrudProjects.ToListAsync();
            studentList.AddRange(data);
            return Ok(studentList);
        }

        // API to get inActive students
        //[HttpGet]

        //public async Task<ActionResult<List<CrudProject>>> InActive()
        //{
        //    List<CrudProject> studentList = new List<CrudProject>();
        //    var data = await _context.CrudProjects.Where(e => 
        //    !e.IsActive)
        //        .ToListAsync();
        //    studentList.AddRange(data);
        //    return Ok(studentList);
        //}

        // API to get Active students
        //[HttpGet]

        //public async Task<ActionResult<List<CrudProject>>> Active()
        //{
        //    List<CrudProject> studentList = new List<CrudProject>();
        //    var data = await _context.CrudProjects.Where(e =>
        //    e.IsActive)
        //        .ToListAsync();
        //    studentList.AddRange(data);
        //    return Ok(studentList);
        //}


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
                Subject = student.Subject,
                IsActive = true,
            };

            _context.CrudProjects.Add(studentData);
            await _context.SaveChangesAsync();

            return Ok(await _context.CrudProjects.ToListAsync());
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<List<CrudProject>>> UpdateStudent([FromRoute] int id, CrudProjectDto request)      
        {
            var dbPupil = await _context.CrudProjects.FindAsync(id);
            if (dbPupil == null)
            return BadRequest("Pupil not found");

            dbPupil.FirstName = request.FirstName;
            dbPupil.LastName = request.LastName;
            dbPupil.Age = Convert.ToInt32(request.Age);
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

            dbPupil.IsActive = false;
            // _context.CrudProjects.Remove(dbPupil);
            await _context.SaveChangesAsync();

            return Ok(await _context.CrudProjects.ToListAsync());
        }

        [HttpPost("{id}")]

        public async Task<ActionResult<List<CrudProject>>> Restore(int id)
        {
            var dbPupil = await _context.CrudProjects.FindAsync(id);
            if (dbPupil == null)
                return BadRequest("Pupil not found");

            dbPupil.IsActive = true;
            // _context.CrudProjects.Remove(dbPupil);
            await _context.SaveChangesAsync();

            return Ok(await _context.CrudProjects.ToListAsync());
        }
    }
}
