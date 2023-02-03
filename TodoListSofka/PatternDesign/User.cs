using System;
using TodoListSofka.Model;

namespace TodoListSofka.PatternDesign
{
    public class User : IObserver
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public User(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public User()
        {
        }
        public void Subscribe(Calendar calendar)
        {
            calendar._usersList.Add(this);
        }

        public void Unsubscribe(Calendar calendar)
        {
            calendar._usersList.Remove(this);
        }

        public void Update(Calendar calendar)
        {
            foreach (var user in calendar._usersList)
            {
                Console.WriteLine($"Usuario: {user.Name}, Se abrió la agenda el día: {calendar.IndexDay}");
            }
            Console.WriteLine("\n");
        }
    }
}
