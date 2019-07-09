using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP7
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Empleado> ListaEmpleados = new List<Empleado>();

            for (int i = 0; i < 20; i++)
            {
                ListaEmpleados.Add(CrearEmpleado(i));
            }

            Console.WriteLine("\nLista General de todos los empleados:");

            float SalarioTotal = 0;
            foreach (Empleado x in ListaEmpleados)
            {
                MostrarEmpleados(x);
                SalarioTotal += Salario(x);
            }

            Console.WriteLine("\nCantidad de empleados que posee la empresa:{0}", ListaEmpleados.Count);
            Console.WriteLine("\nMonto total del salario de todos los empleados:{0}", SalarioTotal);
        }

        public enum Cargo
        {
            Auxiliar,
            Administrativo,
            Ingeniero,
            Especialista,
            Investigador,
        }

        public struct Fecha
        {
            public int d;
            public int m;
            public int y;

            public Fecha(int _d, int _m, int _y)
            {
                d = _d;
                m = _m;
                y = _y;
            }
        }

        public struct Empleado
        {
            public string Nombre;
            public string Apellido;
            public string EstadoCivil;
            public string Genero;
            public float SueldoBasico;
            public int CantidadDeHijos;
            public Fecha FechaDeNacimiento;
            public Fecha FechaDeIngreso;
            public Cargo Puesto;
            public int ID;

            public Empleado(string _Nombre,
                            string _Apellido,
                            Fecha _FechaDeNacimiento,
                            string _EstadoCivil,
                            string _Genero,
                            Fecha _FechaDeIngreso,
                            float _SueldoBasico,
                            Cargo _Cargo,
                            int _CantidadDeHijos, int _ID)
            {
                Nombre = _Nombre;
                Apellido = _Apellido;
                FechaDeNacimiento = _FechaDeNacimiento;
                EstadoCivil = _EstadoCivil;
                Genero = _Genero;
                FechaDeIngreso = _FechaDeIngreso;
                SueldoBasico = _SueldoBasico;
                Puesto = _Cargo;
                CantidadDeHijos = _CantidadDeHijos;
                ID = _ID;
            }
        }

        public static int Antiguedad(int FechaDeIngreso)
        {
            int FechaActual = 2019;
            return FechaActual - FechaDeIngreso;
        }

        public static int EdadDelEmpleado(int FechaDeNacimiento)
        {
            int FechaActual = 2019;
            return FechaActual - FechaDeNacimiento;
        }

        public static int Jubilacion(Empleado Trabajador)
        {
            if (Trabajador.Genero == "Masculino")
            {
                int EdadJub = 65;
                return EdadJub - EdadDelEmpleado(Trabajador.FechaDeNacimiento.y);
            }

            else
            {
                int EdadJub = 60;
                return EdadJub - EdadDelEmpleado(Trabajador.FechaDeNacimiento.y);
            }
        }

        public static float Salario(Empleado Trabajador)
        {
            float Adicional = 0;

            if (Antiguedad(Trabajador.FechaDeIngreso.y) <= 20)
            {
                int Aux = 2 * Antiguedad(Trabajador.FechaDeIngreso.y);
                Adicional = Trabajador.SueldoBasico + Trabajador.SueldoBasico * (Aux / 100);

            }

            else
            {
                int Aux = 25;
                Adicional = Trabajador.SueldoBasico + Trabajador.SueldoBasico * (Aux / 100);
            }

            if (Trabajador.Puesto == Cargo.Especialista || Trabajador.Puesto == Cargo.Ingeniero)
            {
                Adicional = Adicional + Adicional / 2;
            }

            if (Trabajador.EstadoCivil == "Casado" && Trabajador.CantidadDeHijos >= 2)
            {
                Adicional = Adicional + 5000;
            }


            return Trabajador.SueldoBasico + Adicional;
        }

        public static Empleado CrearEmpleado(int i)
        {
            string Nombre;
            string Apellido;
            string Genero;
            Fecha FechaDeNacimiento;
            Fecha FechaDeIngreso;
            string EstadoCivil;
            float SueldoBasico;
            Cargo Puesto;
            int CantidadDeHijos;
            int id;

            id = i;

            string[] NombresM = new string[7] { "Luke", "Sean", "José", "Leandro", "Christopher", "Joel", "Lee" };
            string[] NombresF = new string[7] { "Samantha", "Carla", "Carolina", "Silvina", "Lilith", "Penélope", "Malia" };
            string[] Apellidos = new string[7] { "Jones", "McCall", "Styles", "Arias", "Foster", "Winters", "Thompson" };

            Apellido = Apellidos[RandomNumber(1, 7)];
            int GenderAux = RandomNumber(0, 2);

            if (GenderAux == 1)
            {
                Genero = "Masculino";
                Nombre = NombresM[RandomNumber(1, 7)];
            }
            else
            {
                Genero = "Femenino";
                Nombre = NombresF[RandomNumber(1, 5)];
            }

            if (RandomNumber(0, 2) == 1)
            {
                EstadoCivil = "Casado";
            }
            else
            {
                EstadoCivil = "Soltero";
            }

            SueldoBasico = RandomNumber(9000, 15000);

            Console.WriteLine("Ingrese la cantidad de hijos que posee:");
            CantidadDeHijos = int.Parse(Console.ReadLine());

            FechaDeNacimiento = new Fecha(RandomNumber(1, 31), RandomNumber(1, 12), RandomNumber(1950, 2001));

            FechaDeIngreso = new Fecha(RandomNumber(1, 29), RandomNumber(1, 13), RandomNumber(FechaDeNacimiento.y + 18, 2019));

            switch (RandomNumber(1, 6))
            {
                case 1:
                    Puesto = Cargo.Auxiliar;
                    break;
                case 2:
                    Puesto = Cargo.Administrativo;
                    break;
                case 3:
                    Puesto = Cargo.Especialista;
                    break;
                case 4:
                    Puesto = Cargo.Ingeniero;
                    break;
                case 5:
                    Puesto = Cargo.Investigador;
                    break;
                default:
                    Puesto = Cargo.Auxiliar;
                    break;
            }
            Empleado Trabajador = new Empleado(Nombre, Apellido, FechaDeNacimiento, EstadoCivil, Genero, FechaDeIngreso, SueldoBasico, Puesto, CantidadDeHijos, id);
            return Trabajador;
        }

        public static void MostrarEmpleados(Empleado Trabajador)
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("ID del Empleado:{0}", Trabajador.ID);
            Console.WriteLine("Nombre: {0}", Trabajador.Nombre);
            Console.WriteLine("Apellido: {0}", Trabajador.Apellido);
            Console.WriteLine("Fecha de nacimiento: {0}/{1}/{2}", Trabajador.FechaDeNacimiento.d, Trabajador.FechaDeNacimiento.m, Trabajador.FechaDeIngreso.y);
            Console.WriteLine("Estado civil: {0}", Trabajador.EstadoCivil);
            Console.WriteLine("Género: {0}", Trabajador.Genero);
            Console.WriteLine("Fecha de ingreso: {0}/{1}/{2}", Trabajador.FechaDeIngreso.d, Trabajador.FechaDeIngreso.m, Trabajador.FechaDeIngreso.y);
            Console.WriteLine("Sueldo básico: ${0}", Trabajador.SueldoBasico);
            Console.WriteLine("Cargo: {0}", Trabajador.Puesto);
            Console.WriteLine("Cantidad de hijos: {0}", Trabajador.CantidadDeHijos);
            Console.WriteLine("Edad del Empleado:{0}", EdadDelEmpleado(Trabajador.FechaDeNacimiento.y));
            Console.WriteLine("Salario total del Empleado:{0}", Salario(Trabajador));
            Console.WriteLine("Años faltantes para la jubilacion:{0}", Jubilacion(Trabajador));
            Console.WriteLine("Antiguedad del Empleado (en años): {0}", Antiguedad(Trabajador.FechaDeIngreso.y));
            Console.WriteLine("-------------------------------------------");
        }

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
