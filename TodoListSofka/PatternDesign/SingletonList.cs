using TodoListSofka.DTO;
using TodoListSofka.Models;

namespace TodoListSofka.PatternDesign
{
    public class SingletonList
    {
        private List<Todoitem> _list = new List<Todoitem>();
        public List<Todoitem> List
        {
            get { return _list; }
            set { _list = value; }
        }

        private static SingletonList? _instance;
        private SingletonList() { }
        public static SingletonList GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SingletonList();
                }
                return _instance;
            }
        }

    }
}
