using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace goldencardAPI.Controllers
{
    [Route("api/[controller]")]
    public class HeroesController : Controller
    {
        private Models.HeroContext _context;

        public HeroesController(Models.HeroContext context)
        {
            _context = context;
        }

        // GET api/heroes
        [HttpGet]
        public IEnumerable<Hero> Get()
        {
            return _context
            .Heroes
            .Select(h => new Hero()
            {
                Id = h.Id,
                Name = h.Name
            })
            .ToList();
        }

        // GET api/heroes/5
        [HttpGet("{id}")]
        public Hero Get(int id)
        {
            return _context
                        .Heroes
                        .Select(h => new Hero()
                        {
                            Id = h.Id,
                            Name = h.Name
                        })
                        .FirstOrDefault(h => h.Id == id);
        }

        // POST api/heroes
        [HttpPost]
        public Hero Post([FromBody]Hero hero)
        {
            Models.Hero dbHero = new Models.Hero
            {
                Name = hero.Name
            };

            _context.Heroes.Add(dbHero);
            _context.SaveChanges();

            return new Hero
            {
                Id = dbHero.Id,
                Name = dbHero.Name
            };
        }

        // PUT api/heroes/5
        [HttpPut("{id}")]
        public Hero Put(int id, [FromBody]Hero hero)
        {
            var dbHero = _context.Heroes.First(h => h.Id == id);

            dbHero.Name = hero.Name;

            _context.Heroes.Update(dbHero);
            _context.SaveChanges();

            return hero; 
        }

        // DELETE api/heroes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var dbHero = _context.Heroes.First(h => h.Id == id);
            _context.Heroes.Remove(dbHero);
            _context.SaveChanges();
           
        }
    }

    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
