using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web_App.Pages;
using static Web_App.Pages.Employees;

namespace Web_App
{
    public class UserController
    {
        private readonly UserService _userService;
        private ObservableCollection<User> _users;
        public ObservableCollection<User> Users
        {
            get => _users;
            set => _users = value;
        }
        private User? _currentUser;
        public User? CurrentUser
        {
            get => _currentUser;
            private set => _currentUser = value;
        }
        public UserController()
        {
            _userService = new();
            _users = new ObservableCollection<User>();
        }

        public UserController(UserService userService)
        {
            _userService = userService;
            _users = new ObservableCollection<User>();
        }
        public async Task GetAllUsers()// alle employees pakken.
        {
            Users = await _userService.GetAll();
        }

        public async Task GetAllEmployeesPerCompany(Guid CompanyID)// alle employees pakken van hetzelfde bedrijf.
        {
            Users = await _userService.GetUsersPerCompany(CompanyID);
        }

        public async Task<bool> LogIn(UserCredentials userCredentials)// check voor login.
        {
            CurrentUser = await _userService.GetLogin(userCredentials);
            return CurrentUser != null;
        }

        public void Logout()// uitloggen.
        {
            CurrentUser = null;
        }

        public async Task DeleteUsers(Guid[] UsersToDelete)// lijst met employees die verwijderd moeten worden.
        {
            foreach (var userToDelete in UsersToDelete) 
            {
                await _userService.DeleteUsersDatabase(userToDelete);
            }
            
        }
        public async Task addUser(User user)// employee toevoegen.
        {
            await _userService.addUserDatabase(user);
        }

        public async Task<List<User>> usersWithLunches(Guid CompanyID, DayOfWeek dayOfWeek)// lijst met employees die WEL hun lunches hebben opgegeven voor de volgende week.
        {
            var EmployeesToReturn = await _userService.getUsersWithLunches(CompanyID, dayOfWeek);
            return EmployeesToReturn;
        }

        public async Task<List<User>> usersWithoutLunches(Guid CompanyID, DayOfWeek dayOfWeek)// lijst met employees die NIET hun lunches hebben opgegeven voor de volgende week.
        {
            var EmployeesToReturn = await _userService.getUsersWithoutLunches(CompanyID, dayOfWeek);
            return EmployeesToReturn;
        }

        public async Task<int> getLunchesAmount(Guid employeeID)// Totaal aantal lunches voor één employee.
        {
            var count = await _userService.getLunchesCount(employeeID);
            return count;
        }
    }
}
