using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_App.Services;


namespace Web_App
{
    public class GroceryListController
    {
        private readonly GroceryListService _grocerylistService;


        private ObservableCollection<Grocery> _groceries;
        public ObservableCollection<Grocery> Groceries
        {
            get => _groceries;
            set => _groceries = value;
        }

        public GroceryListController()
        {

            _grocerylistService = new();
            _groceries = new ObservableCollection<Grocery>();
        }

        public async Task GetAllGroceries(Guid CompanyID)//lijst met alle groceries van dat bedrijf.
        {
            Groceries = await _grocerylistService.GetAll(CompanyID);
        }

        public async Task addGrocery(Grocery grocery)
        {
            await _grocerylistService.addGroceryDatabase(grocery);
        }

        public async Task deleteGrocery(Guid GroceryID)
        {
            await _grocerylistService.DeleteGroceryDatabase(GroceryID);
        }
    }
}
