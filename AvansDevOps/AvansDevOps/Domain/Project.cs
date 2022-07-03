using AvansDevOps.Domain.Factories;
using AvansDevOps.Domain.UserRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansDevOps.Domain
{
    public class Project
    {
        public string _name { get; set; }
        public UserFactory _userFactory { get; set; }
        public List<User> _users { get; set; }
        public List<BacklogItem> _backlogItems { get; set; }
        public List<Sprint> _sprints { get; set; }

        public Project(string name, UserFactory userFactory)
        {
            _name = name;
            _userFactory = userFactory;
            _users = new List<User>();
            _backlogItems = new List<BacklogItem>();
            _sprints = new List<Sprint>();
        }

        public void AddUser(string name, string role)
        {
            _users.Add(_userFactory.CreateUser(name, role));
        }

        public void AddBacklogItem(BacklogItem backlogItem)
        {
            _backlogItems.Add(backlogItem);
        }

        public void AddSprint(Sprint sprint)
        {
            _sprints.Add(sprint);
        }
    }
}
