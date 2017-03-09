using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace Controllers
{
    [Route("api/[controller]")]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class HeroesController : Controller
    {
        //private readonly Repositories.HeroRepository _heroRepository;

        public HeroesController()//Repositories.HeroRepository heroRepository)
        {
            //_heroRepository = heroRepository;
        }

        // GET api/heroes
        [HttpGet]
        public IEnumerable<Hero> Get()
        {
            //return //_heroRepository.Get();

            return new List<Hero>{
                new Hero() { Id = 1, Name = "a" },
                new Hero() { Id = 2, Name = "b" },
                new Hero() { Id = 3, Name = "c" },
                new Hero() { Id = 4, Name = "d" }
            };
        }

        // GET api/heroes/5
        [HttpGet("{id}")]
        public Hero Get(int id)
        {
            return new Hero() { Id = id, Name = "a" };
        }

        // POST api/heroes
        [HttpPost]
        public Hero Post([FromBody]Hero hero)
        {
            return hero;
        }

        // PUT api/heroes/5
        [HttpPut("{id}")]
        public Hero Put(int id, [FromBody]Hero hero)
        {
            return hero;
        }

        // DELETE api/heroes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            //_heroRepository.Delete(id);
        }
    }
}
