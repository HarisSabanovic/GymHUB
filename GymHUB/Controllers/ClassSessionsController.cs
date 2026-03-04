using GymHUB.Data;


namespace GymHUB.Controllers
{
    public class ClassSessionsController
    {
        private readonly ApplicationDbContext _db;

        public ClassSessionsController(ApplicationDbContext db) 
        { 
            _db = db; 
        }


    }
}
