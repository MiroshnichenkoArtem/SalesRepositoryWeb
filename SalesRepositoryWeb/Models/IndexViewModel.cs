using System.Collections.Generic;

namespace SalesRepositoryWeb.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Sale> Sales { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public IEnumerable<Manager> SelectedManagers { get; set; }
    }
}