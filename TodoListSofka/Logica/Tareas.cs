namespace TodoListSofka1.Logica
{
	public class Tareas : Calendario
	{
		private static Tareas instance = null;

		public static Tareas Instance
		{
			get 
			{
				if (instance == null) { instance = new Tareas(); }

				return instance;
			}
		}
	}
}
