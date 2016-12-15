using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace Controllers
{
    [Route("api/[controller]")]
    public class HeroesController : Controller
    {
        private readonly Repositories.HeroRepository _heroRepository;

        public HeroesController(Repositories.HeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        // GET api/heroes
        [HttpGet]
        public IEnumerable<Hero> Get()
        {
            return _heroRepository.Get();
        }

        // GET api/heroes/5
        [HttpGet("{id}")]
        public Hero Get(int id)
        {
            return _heroRepository.Get(id);
        }

        // POST api/heroes
        [HttpPost]
        public Hero Post([FromBody]Hero hero)
        {
            return _heroRepository.Add(hero);
        }

        // PUT api/heroes/5
        [HttpPut("{id}")]
        public Hero Put(int id, [FromBody]Hero hero)
        {
            return _heroRepository.Update(id, hero);
        }

        // DELETE api/heroes/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _heroRepository.Delete(id);
        }
    }
}
