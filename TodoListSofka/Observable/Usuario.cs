using System.Globalization;
using TodoListSofka1.Logica;

namespace TodoListSofka1.Observable
{
	public class Usuario: IObserver
	{
		private string name;

		public Usuario (string name)
		{
			this.name = name;
		}
		public Usuario() { }

		public void Unsubscribe(Usuario usuario)
		{
			Calendario.Instance.Unsubscribe(usuario);
		}

		public void Update()
		{
			Console.WriteLine("Se ha actualizado la tarea del dia {0}",Calendario.Instance.Dia);
		}

		public void Create()
		{
			Console.WriteLine("Se creado una nueva tarea el dia {0}", Calendario.Instance.Dia);
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}
	}
}
