﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Server_Things.Models
{
    public enum Role
    {
        SuperAdmin,
        Admin,
        Employee
    }
    public class User
    {
        private string _email;
        public Guid Id { get; init; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }

        public string Email
        {
            get => _email;
            set => _email = value.ToLower();
        }
        public Role Role { get; set; }

        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }

        public Guid? CompanyId { get; set; }
        public List<OfficeDay> DaysAtOffice { get; set; }
        public User(Guid id, string firstName, string lastName, string password, string email, Role role, Company? company)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
            Company = company ?? null;
            Role = role;
            DaysAtOffice = new List<OfficeDay>();
        }
        public User(string firstName, string lastName, string password, string email, Role role, Company? company)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Email = email;
            Company = company ?? null;
            Role = role;
            DaysAtOffice = new List<OfficeDay>();
        }

        public User(string firstName, string lastName, string password, string email, Role role, Company? company,
            List<OfficeDay> officeDays) : this(firstName, lastName, password, email, role, company)
        {
            DaysAtOffice = officeDays;
        }
        public User(string firstName, string lastName, string password, string email, Role role, Guid? companyId,
            List<OfficeDay> officeDays) : this(firstName, lastName, password, email, role, null)
        {
            DaysAtOffice = officeDays;
            Company = null;
            CompanyId = companyId;
        }

        public User()
        {

        }
    }
}
