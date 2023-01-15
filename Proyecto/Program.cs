using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace Proyecto
{
    class Program
    {
        class nota
        // Clase para definir una nota
        {
            public int frecuencia;
            public int duracion;
        }

        class listanota
        // Clase para definir una canción, o una lista de notas
        {
            public nota[] cancion = new nota[MAX];
            public int num = 0;
        }

        static void leerteclas(char[] tec, int[] frec)
        //Procedimiento para leer fichero de notas e introducirlo en dos vectores
        {
            StreamReader f = new StreamReader("teclas.txt");
            string[] tecla = new string [20];
            int i = 0;

            while (i < 20)
            {
                string lineas = f.ReadLine();
                string[] trozos = lineas.Split(' ');
                tec[i] = Convert.ToChar(trozos[0]);
                frec[i] = Convert.ToInt32(trozos[1]);
                tecla[i] = trozos[2];
                i = i + 1;
            }
            f.Close();
            i = 0;
            while (i < 20)
            {
                Console.WriteLine(tec[i] + " es la nota " + tecla[i]);
                i++;
            }
            Console.WriteLine("Para dejar de tocar pulse x");
        }

        static int teclas(char[] tec, int[] frec, char m)
            //Función para comprobar si la tecla pulsada contiene una nota o no. 
            // Devuelve la frecuencia de la nota, o un 0 en caso de que la tecla pulsada no contenga una nota asignada
        {
            int res = 0;
            int i = 0;
            bool encontrado = false;

            while ((i < 20) && (encontrado == false))
            {
                if (m == tec[i])
                {
                    res = frec[i];
                    encontrado = true;
                }

                else
                    i = i + 1;
            }
            return res;
        }

        static void repcancion(listanota lista, int a)
            //Procedimiento para reproducir una canción
        {
            int i = 0;

            while (i < a)
            {
                Console.Beep(lista.cancion[i].frecuencia, 1000);
                i = i + 1;
            }
        }

        static void escribircanciones()
            //Procedimiento para escribir una nueva canción en la lista de canciones
        {
            StreamReader f = new StreamReader("canciones.txt");
            int i = 0;
            int c = Convert.ToInt32(f.ReadLine());
            Console.WriteLine("Las canciones disponibles son:");
            while (i < c)
            {
                Console.WriteLine(f.ReadLine());
                i++;
            }
            f.Close();
        }

        static void guarcancion(listanota lista, int a)
            //Procedimiento para guardar cancion
        {
            Console.WriteLine("Escribe el nombre de la cancion:");
            string nom = Console.ReadLine();
            int i = 0;

            StreamWriter f = new StreamWriter(nom + ".txt");
            StreamReader b = new StreamReader("canciones.txt");
            int c = Convert.ToInt32(b.ReadLine());
            string[] canciones = new string [c];
            while (i < c)
            {
                canciones[i] = b.ReadLine();
                i++;
            }
            b.Close();
            StreamWriter h = new StreamWriter("canciones.txt");
            i = 0;
            bool encontrado = false;
            while ((i < c) && (encontrado == false))
            {
                if (canciones[i] == nom)
                    encontrado = true;
                i = i + 1;
            }
            if (encontrado == true)
            {
                h.WriteLine(c);
                i = 0;
                while (i < c)
                {
                    h.WriteLine(canciones[i]);
                    i++;
                }
                h.Close();
            }
            else
            {
                c = c + 1;
                h.WriteLine(c);
                i = 0;

                while (i < c - 1)
                {
                    h.WriteLine(canciones[i]);
                    i++;
                }
                h.WriteLine(nom);
                h.Close();
            }
            f.WriteLine(a);
            i = 0;

            while (i < a)
            {
                f.WriteLine(lista.cancion[i].frecuencia);
                i = i + 1;
            }
            f.Close();
        }
        static void eliminarcancion()
            //Procedimiento para eliminar una canción guardada
        {
            Console.WriteLine("Escribe el nombre de la canción que desea eliminar.");
            string nom = Console.ReadLine();
            StreamReader f = new StreamReader("canciones.txt");
            int c = Convert.ToInt32(f.ReadLine());
            string[] canc = new string[c];
            int i = 0;

            while (i < c)
            {
                canc[i] = f.ReadLine();
                i++;
            }
            f.Close();
            i = 0;
            bool encontrado = false;

            while ((i < c) && (encontrado == false))
            {
                if (canc[i] == nom)
                    encontrado = true;
                else
                    i++;
            }

            if (encontrado == false)
                Console.WriteLine("No se ha encontrado esta canción.");
            else
            {
                File.Delete(nom + ".txt");
                c = c - 1;
                StreamWriter h = new StreamWriter("canciones.txt");
                canc[i] = canc[c];
                h.WriteLine(c);
                i = 0;
                while (i < c)
                {
                    h.WriteLine(canc[i]);
                    i++;
                }
                h.Close();
                Console.WriteLine("Canción eliminada.");
            }
        }
        const int MAX = 100;
        static void Main(string[] args)
        {
            int a = 0;
            Console.WriteLine("              -----------------------------------------------");
            Console.WriteLine("              |            SIMULADOR DE PIANO               |");
            Console.WriteLine("              -----------------------------------------------");
            while (a != 5)
            {
                Console.WriteLine("Pulse 1 si quiere tocar el piano.");
                Console.WriteLine("Pulse 2 si quiere escuchar una canción guardada.");
                Console.WriteLine("Pulse 3 si quiere editar una canción guardada.");
                Console.WriteLine("Pulse 4 si quiere eliminar una canción guardada.");
                Console.WriteLine("Pulse 5 si quiere salir.");
                Console.WriteLine("Presiona una opción:");
                try
                {
                    a = Convert.ToInt32(Console.ReadLine());
                    if (a == 1)
                {
                    listanota lista = new listanota();
                    int[] frec = new int[20];
                    char[] tec = new char[20];
                    leerteclas(tec, frec);
                    int res = 0;

                    bool encontrado = false;

                    while ((lista.num < MAX) && (encontrado == false))
                    {
                        Console.WriteLine("Pulse una tecla para tocar una nota del piano.");
                        try
                        {
                            char m = Convert.ToChar(Console.ReadLine());
                            res = teclas(tec, frec, m);
                            if (m == 'x')
                                encontrado = true;

                            else if (res != 0)
                            {
                                nota l = new nota();
                                Console.Beep(res, 1000);
                                l.frecuencia = res;
                                l.duracion = 1000;
                                lista.cancion[lista.num] = l;
                                lista.num = lista.num + 1;
                            }

                            else
                                Console.WriteLine("La tecla presionada no tiene una nota asignada.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("El formato es incorrecto. Solo debe presionar una tecla");
                        }
 
                    }
                    if (encontrado == false)
                        Console.WriteLine("El espacio de la canción está lleno.");
                    int b = 0;
                    encontrado = false;
                    while ((b != 3) && (encontrado == false))
                    {
                        Console.WriteLine("Si desea reproducirla pulse 1, si desea guardarla pulse 2, si desea volver al menú pulse 3.");
                        try
                        {
                            b = Convert.ToInt32(Console.ReadLine());

                            if (b == 1)
                            {
                                repcancion(lista, lista.num);
                            }

                            else if (b == 2)
                            {
                                guarcancion(lista, lista.num);
                                encontrado = true;
                                Console.WriteLine("Canción guardada.");
                            }

                            else if (b == 3) ;
                            else
                                Console.WriteLine("Esa opción no está disponible.");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("El formato no es correcto. Debe presionar un número.");
                        }
                    }
                }
                else if (a == 2)
                {
                    listanota lista = new listanota();
                    StreamReader f;
                    int i = 0;
                    escribircanciones();
                    Console.WriteLine("Escribe el nombre de la canción que quieres reproducir:");
                    string nom = Console.ReadLine();
                    try
                    {
                        f = new StreamReader(nom + ".txt");
                        lista.num = Convert.ToInt32(f.ReadLine());
                        i = 0;
                        while (i < lista.num)
                        {
                            nota l = new nota();
                            l.frecuencia = Convert.ToInt32(f.ReadLine());
                            l.duracion = 1000;
                            lista.cancion[i] = l;
                            i = i + 1;
                        }
                        f.Close();
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("No se ha encontrado esta canción.");
                    }
                    repcancion(lista, lista.num);
                }

                else if (a == 3)
                {
                    escribircanciones();
                    Console.WriteLine("Escribe el nombre de la canción que quieres editar:");
                    string nom = Console.ReadLine();

                    StreamReader f;
                    try
                    {
                        f = new StreamReader(nom + ".txt");
                        listanota lista = new listanota();
                        int[] frec = new int[20];
                        char[] tec = new char[20];
                        leerteclas(tec, frec);
                        lista.num = Convert.ToInt32(f.ReadLine());
                        int i = 0;
                        while (i < lista.num)
                        {
                            nota l = new nota();
                            l.frecuencia = Convert.ToInt32(f.ReadLine());
                            l.duracion = 1000;
                            lista.cancion[i] = l;
                            i = i + 1;
                        }
                        f.Close();

                        bool encontrado = false;
                        int res = 0;

                        while ((lista.num < MAX) && (encontrado == false))
                        {
                            Console.WriteLine("Presiona una nota del piano para añadirla a la canción.");
                            try
                            {
                                char m = Convert.ToChar(Console.ReadLine());
                                res = teclas(tec, frec, m);
                                if (m == 'x')
                                    encontrado = true;

                                else if (res != 0)
                                {
                                    Console.Beep(res, 1000);
                                    nota p = new nota();
                                    p.frecuencia = res;
                                    p.duracion = 1000;
                                    lista.cancion[lista.num] = p;
                                    lista.num = lista.num + 1;
                                }

                                else
                                    Console.WriteLine("La tecla presionada no tiene una nota asignada.");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("El formato no es correcto. Debe presionar una tecla.");
                            }
                        }

                        if (encontrado == false)
                            Console.WriteLine("El espacio de la canción está lleno.");
                        int b = 0;
                        encontrado = false;
                        while ((b != 3) && (encontrado == false))
                        {
                            Console.WriteLine("Si desea reproducirla pulse 1, si desea guardarla pulse 2, si desea salir pulse 3 para volver al menú.");
                            try
                            {
                                b = Convert.ToInt32(Console.ReadLine());
                                if (b == 1)
                                    repcancion(lista, lista.num);

                                else if (b == 2)
                                {
                                    guarcancion(lista, lista.num);
                                    encontrado = true;
                                    Console.WriteLine("Canción guardada.");
                                }

                                else if (b == 3) ;

                                else
                                    Console.WriteLine("Esta opción no está disponible.");
                            }
                            catch
                            {
                                Console.WriteLine("Este formato no es correcto. Debe pulsar un número.");
                            }
                        }

                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("No se ha encontrado esta canción.");
                    }
                }
                else if (a == 4)
                {
                    escribircanciones();
                    eliminarcancion(); 
                }
                else if (a == 5) ;
                else
                    Console.WriteLine("Esta opción no está disponible.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("El formato no es correcto. Debe presionar un número.");
                }

                
            }
        }
        }
    }
