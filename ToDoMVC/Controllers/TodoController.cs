using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoMVC.Models;

namespace ToDoMVC.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Todo
        public ActionResult Index(TodosViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var TodoViewModel = new TodosViewModel();
            TodoViewModel.Todos = db.Todos.Where(todo => todo.UserId == userId).ToList();
            if(model.SelectedItem == "Todo")
            {
                TodoViewModel.Todos = TodoViewModel.Todos.Where(todo => todo.Complete == false).ToList();
            }
            else if(model.SelectedItem == "Complete")
            {
                TodoViewModel.Todos = TodoViewModel.Todos.Where(todo => todo.Complete == true).ToList();
            }
            else
            {
                TodoViewModel.Todos = TodoViewModel.Todos.ToList();
            }
            if(model.DueDate == "Due today")
            {
                var today = DateTime.Today;
                TodoViewModel.Todos = TodoViewModel.Todos.Where(todo => todo.DueDate == today).ToList();
            }
            else if(model.DueDate == "Due by tomorrow")
            {
                var tomorrow = DateTime.Today.AddDays(1);
                TodoViewModel.Todos = TodoViewModel.Todos.Where(todo => todo.DueDate <= tomorrow).ToList();
            }

            else if (model.DueDate == "Due within 7 days")
            {
                var week = DateTime.Today.AddDays(7);
                TodoViewModel.Todos = TodoViewModel.Todos.Where(todo => todo.DueDate <= week).ToList();
            }
            TodoViewModel.SelectedItem = model.SelectedItem;
            if(model.OrderOption == "Priority")
            {
              TodoViewModel.Todos = TodoViewModel.Todos.OrderBy(todo => todo.Priority).ToList();
            }
            else if(model.OrderOption == "Due date")
            {
                TodoViewModel.Todos = TodoViewModel.Todos.OrderBy(todo => todo.DueDate).ToList();
            }
            else if(model.OrderOption == "Created")
            {
                TodoViewModel.Todos = TodoViewModel.Todos.OrderBy(todo => todo.Created).ToList();

            }
            TodoViewModel.Completed = TodoViewModel.Todos.Count(todo => todo.Complete);
            TodoViewModel.NotCompleted = TodoViewModel.Todos.Count(todo => !todo.Complete);
            return View(TodoViewModel);
       
        }

        // GET: Todo/Details/5
        public ActionResult Details(int id)
        {
            var Todo = db.Todos.Find(id);
            return View(Todo);
        }

        // GET: Todo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        [HttpPost]
        public ActionResult Create(Todo todo)
        {
            try
            {
                todo.UserId = User.Identity.GetUserId();
                db.Todos.Add(todo);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Todo/Edit/5
        public ActionResult Edit(int id)
        {
            var Todo = db.Todos.Find(id);
            return View(Todo);
        }

        // POST: Todo/Edit/5
        [HttpPost]
        public ActionResult Edit(Todo todo)
        {
            try
            {
                db.Todos.Add(todo);
                db.Entry(todo).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Todo/Delete/5
        public ActionResult Delete(int id)
        {
            var itemtodelete = db.Todos.Find(id);
            db.Todos.Remove(itemtodelete);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
