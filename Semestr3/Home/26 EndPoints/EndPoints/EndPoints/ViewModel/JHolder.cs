using EndPoints.Models;
using EndPoints.ViewModel.Commands;
using EndPoints.ViewModel.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EndPoints.ViewModel
{
    public class JHolder
    {
        public ObservableCollection<User> Users { set; get; } = new ObservableCollection<User>();
        public ObservableCollection<Todo> Todos { set; get; } = new ObservableCollection<Todo>();

        public CommandSaveUsers CommandSaveUsers { set; get; }
        public CommandLoadUsers CommandLoadUsers { set; get; }
        public CommandLoadTodos CommandLoadTodos { set; get; }

        public JHolder()
        {
            CommandSaveUsers = new CommandSaveUsers(this);
            CommandLoadUsers = new CommandLoadUsers(this);
            CommandLoadTodos = new CommandLoadTodos(this);
        }
        public async void GetUsersAsync()
        {
            var users = await JsonplaceholderHelper.GetUsersAsync<User>("users");
          
            Users.Clear();
            foreach (var i in users)
                Users.Add(i);
       
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
        }
        public async void GetTodosAsync()
        {
            var todos = await JsonplaceholderHelper.GetUsersAsync<Todo>("todos");

            Todos.Clear();
            foreach (var i in todos)
                Todos.Add(i);
        }

        public async void SaveUsersAsync()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("users.jsn", false, System.Text.Encoding.Default))
                {
                    await sw.WriteLineAsync(JsonConvert.SerializeObject(Users, Formatting.Indented));
                }
                MessageBox.Show("Saved!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
