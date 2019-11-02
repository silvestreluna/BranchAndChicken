using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BranchAndChicken.Api.Commands;
using BranchAndChicken.Api.DataAccess;
using BranchAndChicken.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BranchAndChicken.Api.Controllers
{
    [Route("api/trainers")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Trainer>> GetAllTrainers()
        {
            var repo = new TrainerRepository();
            return repo.GetAll();
        }

        [HttpGet("{name}")]
        public ActionResult<Trainer> GetByName(string name)
        {
            var repo = new TrainerRepository();
            return repo.Get(name);
        }

        [HttpDelete("{name}")]
        public IActionResult DeleteTrainer(string name)
        {
            var repo = new TrainerRepository();
            repo.Remove(name);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTrainer(UpdateTrainerCommand updatedTrainerCommand, int id)
        {
            var repo = new TrainerRepository();

            var updatedTrainer = new Trainer
            {
                Name = updatedTrainerCommand.Name,
                YearOfExperience = updatedTrainerCommand.YearOfExperience,
                Specialty = updatedTrainerCommand.Specialty,
            };

            var updatedRepoTrainer = repo.Update(updatedTrainer, id);

            if (updatedRepoTrainer == null)
            {
                return NotFound("Could not update trainer");
            }

            return Ok(updatedRepoTrainer);
        }

        //[HttpPost]
        //public IActionResult CreateTrainer(AddTrainerCommand newTrainerCommand)
        //{
        //    var newTrainer = new Trainer
        //    {
        //      //  Id = Guid.NewGuid(),
        //        Name = newTrainerCommand.Name,
        //        YearOfExperience = newTrainerCommand.YearOfExperience,
        //        Specialty = newTrainerCommand.Specialty,
        //    };

        //    var repo = new TrainerRepository();
        //    var trainerThatGotCreated = repo.Add(newTrainer);
        //    return Created($"api/trainers/{trainerThatGotCreated.Name}", trainerThatGotCreated);
        //}

    }
}