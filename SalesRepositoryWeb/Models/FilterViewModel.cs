using System.Collections.Generic;
using System.Web.Mvc;

namespace SalesRepositoryWeb.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<Manager> managers,int? manager, string productName)
        {
            managers.Insert(0, new Manager { Lastname = "Все", Id = 0 });
            Managers = new SelectList(managers, "Id", "Name", manager);
            SelectedManager = manager;
            SelectedProductName = productName;
        }
        public SelectList Managers { get; private set; } 
        public int? SelectedManager { get; private set; }  
        public string SelectedProductName { get; private set; }    
    }
}