using TodoListSofka1.Logica;

namespace TodoListSofka1.Observable
{
	public interface IObserver
	{
		void Update();
		void Unsubscribe(Usuario usuario);
	}
}
