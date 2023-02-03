using TodoListSofka1.Observable;

namespace TodoListSofka1.Logica
{
	public class Tareas
	{
		private static Tareas instance = null;
		Usuario usuario = new Usuario();

		public static Tareas Instance
		{
			get 
			{
				if (instance == null) { instance = new Tareas(); }

				return instance;
			}
		}


		public void crearUsuarios()
		{
			usuario.Name = "Juan";
			Calendario.Instance.Subscribe(usuario);
			usuario.Name = "Pedro";
			Calendario.Instance.Subscribe(usuario);
			usuario.Name = "Pablo";
			Calendario.Instance.Subscribe(usuario);
		}

		public void notificarEvento(string evento)
		{
			foreach (var item in Calendario.Instance.Observadores)
			{
				switch (evento)
				{
					case "Crear":
						item.Create();
						break;
					case "Actualizar":
						item.Update();
						break;
				}
			}
		}
	}
}
