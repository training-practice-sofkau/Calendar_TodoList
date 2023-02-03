using TodoListSofka.DTO;

namespace TodoListSofka.PatternDesign
{
    public class CreatorList
    {
        private List<GetFechaDTO> _listItems = new();
        public List<GetFechaDTO> ListItems { get => _listItems; set => _listItems = value; }

        private List<GetTareaDTO> _listItems2 = new();
        public List<GetTareaDTO> ListItems2 { get => _listItems2; set => _listItems2 = value; }
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
