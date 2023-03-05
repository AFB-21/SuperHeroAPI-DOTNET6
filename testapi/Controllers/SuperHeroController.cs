using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace testapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        //private static List<SuperHero> heroes = new List<SuperHero>
        //    {
        //        new SuperHero {
        //            Id = 0,
        //            Name= "SpiderMan",
        //            FirstName="Peter",
        //            LastName="Parker",
        //            Place="New York City"},
        //        new SuperHero
        //        {
        //            Id = 1,
        //            Name="BatMan",
        //            FirstName="Bruce",
        //            LastName="Wayen",
        //            Place="Gotham"
        //        },
        //        new SuperHero
        //        {
        //            Id=2,
        //            Name="SuperMan",
        //            FirstName="Clark",
        //            LastName="Kent",
        //            Place="Metropolis"
        //        },
        //        new SuperHero
        //        {
        //            Id=3,
        //            Name="IronMan",
        //            FirstName="Tony",
        //            LastName="Stark",
        //            Place="LongIsland"
        //        }
        //    };
        private readonly DataContext _context;
        public SuperHeroController(DataContext context)
        {
            _context = context;
        }
            
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.superHeroes.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero =await _context.superHeroes.FindAsync(id);
            if(hero == null)
                return NotFound();
            return Ok(hero);
        }
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = await _context.superHeroes.FindAsync(request.Id);
            if (hero == null)
                return NotFound();
            hero.Name = request.Name;
            hero.FirstName = request.FirstName;
            hero.LastName = request.LastName;
            hero.Place = request.Place;
            await _context.SaveChangesAsync();
            return Ok(await _context.superHeroes.ToListAsync());
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<SuperHero>> Delete(int id)
        {
            var hero = await _context.superHeroes.FindAsync(id);
            if (hero == null)
                return NotFound();
            _context.superHeroes.Remove(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.superHeroes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.superHeroes.Add(hero);
            await _context.SaveChangesAsync();
            return Ok(await _context.superHeroes.ToListAsync());
        }
    }
}
