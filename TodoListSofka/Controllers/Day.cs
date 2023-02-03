using Microsoft.AspNetCore.Mvc;

namespace TodoListSofka.Controllers
{
    public class Day
    {
        public string name;
        public int numberDay;
        public string jorn;

        public Day(string name, int? numberDay, string jorn)
        {
            this.name = name;
            this.numberDay = (int)numberDay;
            this.jorn = jorn;
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

        public string Name { get => name; set => name = value; }
        public int NumberDay { get => numberDay; set => numberDay = value; }
        public string Jorn { get => jorn; set => jorn = value; }


        public override string? ToString()
        {
            return $" la tarea con nombre: {Name} Es para el {NumberDay} de febrero y es en la jornada de la {Jorn}";
        }


        //SINGLETON
        /*
        private static Day instancia;

        private Day()
        {

        }

        public static Day getInstance()
        {

            if (instancia == null)
            {

                instancia = new Day();

            }
            return instancia;

        }
        */


    }
}
