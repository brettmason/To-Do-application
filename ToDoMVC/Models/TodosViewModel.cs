using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDoMVC.Models
{
    public class TodosViewModel
    {
        public ICollection<Todo> Todos { get; set; }
        public SelectList Filters
        {
            get
            {
                return new SelectList(new List<string>()
                {
                    "All", "Todo", "Complete"
                });
            }
        }

        public SelectList DueDateFilters
        {
            get
            {
                return new SelectList(new List<string>()
                {
                   "Due anytime", "Due today", "Due by tomorrow", "Due within 7 days"
                });
            }
        }
        public SelectList OrderOptions
        {
            get
            {
                return new SelectList(new List<string>()
                {
                    "Priority", "Due date", "Created"
                });
            }
        }
        public string SelectedItem { get; set; }
        public string DueDate { get; set; }
        public string OrderOption { get; set; }
        public int Completed { get; set; }
        public int NotCompleted { get; set; }
    
    }
}