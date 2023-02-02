using TodoListSofka.DTO;

namespace TodoListSofka.PatternDesign
{
    public class CreatorList
    {
        private List<GetFechaDTO> _listItems = new();
        public List<GetFechaDTO> ListItems { get => _listItems; set => _listItems = value; }
        public CreatorList() { }

        public static CreatorList _instance;
        public static CreatorList GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CreatorList();
            }
            return _instance;
        }
    }
}
