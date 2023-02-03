
using TodoListSofka.Models;
using TodoListSofka1.Observable;

namespace TodoListSofka1.Logica
{
	public class Calendario: IObservable
	{
		private List<Usuario> observadores = new List<Usuario>();
		private int dia;
		private static Calendario instance = null;

		public static Calendario Instance
		{
			get
			{
				if (instance == null) { instance = new Calendario(); }

				return instance;
			}
		}

		public Calendario()
		{
		}

		public void Subscribe(Usuario observador)
		{
			observadores.Add(observador);
		}

		public void Unsubscribe(Usuario observador)
		{
			observadores.Remove(observador);
		}

		public List<Usuario> Observadores
		{
			get { return observadores; }
		}

		public int Dia
		{
			get { return dia; }
			set { dia = value; }
		}
	}
}
