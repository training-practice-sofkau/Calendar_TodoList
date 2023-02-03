using Microsoft.AspNetCore.Mvc;

namespace TodoListSofka.Controllers
{
    public class Day
    {
        public string name;
        public int numberDay;
        public string jorn;

        public string Name { get => name; set => name = value; }
        public int NumberDay { get => numberDay; set => numberDay = value; }
        public string Jorn { get => jorn; set => jorn = value; }

        public Day(string name, int? numberDay, string jorn)
        {
            this.name = name;
            this.numberDay = (int)numberDay;
            this.jorn = jorn;
        }

        public override string? ToString()
        {
            return $" la tarea con nombre: {Name} Es para el {NumberDay} de febrero y es en la jornada de {Jorn}";
        }

        public static IJornada CreateJornada(string typeJornada)
        {
            switch (typeJornada)
            {

                case "mañana":
                    return new JornadaMorning();

                    break;


                case "noche":
                    return new JornadaNigth();

                    break;

                default:

                   
                    return null;

            }

        }



    }
}
