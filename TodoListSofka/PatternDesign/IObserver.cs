using TodoListSofka.Model;

namespace TodoListSofka.PatternDesign
{
    public interface IObserver
    {
        public void Subscribe(Calendar calendar);
        public void Unsubscribe(Calendar calendar);
        public void Update(Calendar calendar);
    }
}