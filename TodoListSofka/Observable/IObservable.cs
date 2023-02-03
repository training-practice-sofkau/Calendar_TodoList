namespace TodoListSofka1.Observable
{
	public interface IObservable
	{
		void Subscribe(Usuario usuario);
		void Unsubscribe(Usuario usuario);
	}
}
