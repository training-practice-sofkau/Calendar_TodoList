using System.Globalization;
using TodoListSofka1.Logica;

namespace TodoListSofka1.Observable
{
	public class Usuario: IObserver
	{
		private string name;
		public Usuario() { }

		public Usuario(string name)
		{
			Name = name;
		}

		public void Unsubscribe(Usuario usuario)
		{
			Calendario.Instance.Unsubscribe(usuario);
		}

		public void Update(Calendario tarea)
		{
			Console.WriteLine("El nombre del inversor es: {0}", Calendario.Instance.Dia);
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}
	}
}
