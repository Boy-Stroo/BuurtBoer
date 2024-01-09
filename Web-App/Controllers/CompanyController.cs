using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_App
{
    public class CompanyController
    {
        private readonly CompanyService _companyService;
        private ObservableCollection<Company> _companies;
        public ObservableCollection<Company> Companies
        {
            get => _companies;
            set => _companies = value;
        }
        private Company? _currentCompany;
        public Company? CurrentCompany
        {
            get => _currentCompany;
            private set => _currentCompany = value;
        }
        public CompanyController()
        {
            _companyService = new();
            _companies = new ObservableCollection<Company>();
        }

        public CompanyController(CompanyService userService)
        {
            _companyService = userService;
            _companies = new ObservableCollection<Company>();
        }
        public async Task GetAllCompanies()
        {
            Companies = await _companyService.GetAll();
        }

        public async Task DeleteCompanies(Guid[] CompaniesToDelete)
        {
            foreach (var companyToDelete in CompaniesToDelete)
            {
                await _companyService.DeleteCompaniesDatabase(companyToDelete);
            }

        }
    }
}
