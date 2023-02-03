namespace TodoListSofka1.Logica
{
	public class Tareas
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


		public void updateCalendario()
		{
			Console.WriteLine("Tarea actualizada");
		}
	}
}
