namespace active_record
{
    using System;
    using System.Globalization;

    class Program
    {

        private static Person found;

        private static DateTime parseDate(string date)
        {
            var cultureInfo = new CultureInfo("ar-ES");
            return DateTime.Parse(date, cultureInfo);
        }

        static void Main(string[] args)
        {
            bool run = true;
            while (run)
            {
                Console.WriteLine("Menu");
                Console.WriteLine("1. Crear una persona");
                Console.WriteLine("2. Buscar una persona");
                Console.WriteLine("3. Modificar la persona encontrada");
                Console.WriteLine("4. Borrar la persona encontrada");
                Console.WriteLine("5. Crear una frase de la persona encontrada");
                Console.WriteLine("0. Salir");
                int option = Int32.Parse(Console.ReadLine());        

                switch (option)
                {
                    case 1:
                        Person p = new Person();

                        Console.WriteLine("Nombre:");
                        p.FirstName = Console.ReadLine();

                        Console.WriteLine("Apellido:");
                        p.LastName  = Console.ReadLine();

                        Console.WriteLine("Fecha de nacimiento:");
                        string bth = Console.ReadLine();
                        if (bth != "") p.Birth = parseDate(bth);
                        p.Save();
                        break;
                    case 2:
                        int id = Int32.Parse(Console.ReadLine());

                        found = Person.getById(id);
                        if (found == null) Console.WriteLine("No se encontró una persona con ID {0}", id);
                        else               found.Print();
                        break;
                    case 3:
                        Console.WriteLine("Nombre [{0}]: ", found.FirstName);
                        string fn = Console.ReadLine();
                        if (fn == "") fn = found.FirstName;
                        Console.WriteLine("Apellido [{0}]: ", found.LastName);
                        string ln = Console.ReadLine();
                        if (ln == "") ln = found.LastName;
                        Console.WriteLine("Fecha de nacimiento [{0}]: ", found.Birth.ToString("dd/MM/yyyy"));
                        string bd = Console.ReadLine();
                        DateTime birth;
                        if (bd == "") birth = found.Birth;
                        else          birth = parseDate(bd);
                        found.FirstName = fn;
                        found.LastName  = ln;
                        found.Birth     = birth;
                        found.Save();
                        break;
                    case 4:
                        found.Delete();
                        break;
                    case 5:
                        Phrase phrase = new Phrase();
                        Console.WriteLine("Frase:");
                        phrase.PhraseText = Console.ReadLine();
                        phrase.PersonId   = (int) found.PersonId;
                        phrase.Save();
                        break;
                    case 0: 
                        run = false;
                        break;
                    default:
                        break;
                }
            }

            Database.closeConnection();
        }
    }
}
