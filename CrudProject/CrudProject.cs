namespace CrudProject
{
    public class CrudProject
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public int Age { get; set; }
        public string Subject { get; set; }
        
        public bool IsActive { get; set; } = true;
    }

    public class CrudProjectDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        public string Subject { get; set; }
    }
}
