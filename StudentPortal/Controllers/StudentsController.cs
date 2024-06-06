using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortal.Data;
using StudentPortal.Models;
using StudentPortal.Models.Entities;

namespace StudentPortal.Controllers
{
    public class StudentsController : Controller
    {

        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        
        }
        [HttpGet]
        public IActionResult Add()
        { 

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel ViewModel)
        {
            var student = new Students
            {
                Name = ViewModel.Name,
                Email = ViewModel.Email,
                Phone = ViewModel.Phone,
                Subscribed = ViewModel.Subscribed,
            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List() {

          var students = await dbContext.Students.ToListAsync(); 

            return View(students);
        
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) {
            var student = await dbContext.Students.FindAsync(id);

            return View(student);
        
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Students viewModel)
        {
            var students = await dbContext.Students.FindAsync(viewModel.Id);

                if(students is not null)
            {
                students.Name = viewModel.Name;
                students.Email = viewModel.Email;
                students.Phone = viewModel.Phone;
                students.Subscribed = viewModel.Subscribed;

                await dbContext.SaveChangesAsync();

            }

            return RedirectToAction("List","Students");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Students viewModel) {

            var students = await dbContext.Students.FindAsync(viewModel.Id);

            if(students is not null)
            {
                dbContext.Students.Remove(students);

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        
        }

    }
}
