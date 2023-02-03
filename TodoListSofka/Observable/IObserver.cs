using TodoListSofka1.Logica;

namespace TodoListSofka1.Observable
{
	public interface IObserver
	{
		void Update(Calendario tareas);
		void Unsubscribe(Usuario usuario);
	}
}
