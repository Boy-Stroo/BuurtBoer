using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async Task GetAllUsers()
        {
            Users = await _userService.GetAll();
        }

        public async Task GetAllEmployeesPerCompany(Guid CompanyID)
        {
            Users = await _userService.GetUsersPerCompany(CompanyID);
        }

        public async Task<bool> LogIn(UserCredentials userCredentials)
        {
            CurrentUser = await _userService.GetLogin(userCredentials);
            return CurrentUser != null;
        }

        public async Task DeleteUsers(List<DataModel> UsersToDelete)
        {
            await _userService.DeleteUsersDatabase(UsersToDelete);
        }
    }
}
