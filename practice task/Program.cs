class Program
{
    static void Main()
    {
        EmployeeList list = new();

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=-=-=-= MENU =-=-=-=");
            Console.WriteLine("1. Add");
            Console.WriteLine("2. Remove first");
            Console.WriteLine("3. Show all");
            Console.WriteLine("4. Edit by index");
            Console.WriteLine("5. Avg salary");
            Console.WriteLine("6. Find by index");
            Console.WriteLine("7. Save to file");
            Console.WriteLine("8. Load from file");
            Console.WriteLine("9. Show remote employees with salary below average");
            Console.WriteLine("0. Exit");
            Console.Write("Select: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    list.AddToEnd(CreateEmployee());
                    break;
                case "2":
                    list.RemoveFromStart();
                    Console.WriteLine("Removed.");
                    break;
                case "3":
                    list.PrintTable();
                    break;
                case "4":
                    Console.Write("Index to edit: ");
                    if (int.TryParse(Console.ReadLine(), out int idx) && list.SetAt(idx, CreateEmployee()))
                        Console.WriteLine("Updated.");
                    else
                        Console.WriteLine("Invalid index.");
                    break;
                case "5":
                    Console.WriteLine($"Average salary: {list.AverageSalary():F2}");
                    break;
                case "6":
                    Console.Write("Enter index: ");
                    if (int.TryParse(Console.ReadLine(), out int i))
                    {
                        var emp = list.GetAt(i);
                        if (emp != null)
                        {
                            Console.WriteLine($"Department: {emp.Department}");
                            Console.WriteLine($"Salary: {emp.Salary:F2}");
                            Console.WriteLine($"Remote: {emp.RemoteWork}");
                        }
                        else Console.WriteLine("Not found.");
                    }
                    break;
                case "7":
                    list.SaveToFile("employees.json");
                    break;
                case "8":
                    list.LoadFromFile("employees.json");
                    break;
                case "9":
                    list.SearchCriteria();
                    break;
                case "0":
                    list.SaveToFile("employees.json");
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid input.");
                    break;
            }
        }
    }

    static Employee CreateEmployee()
    {
        Department dep;
        while (true)
        {
            Console.Write("Department (HR, IT, Marketing, Sales, Finance): ");
            if (Enum.TryParse(Console.ReadLine(), true, out dep)) break;
            Console.WriteLine("Invalid department.");
        }

        double salary;
        while (true)
        {
            Console.Write("Salary: ");
            if (double.TryParse(Console.ReadLine(), out salary) && salary >= 0) break;
            Console.WriteLine("Invalid salary.");
        }

        bool remote;
        while (true)
        {
            Console.Write("Remote (true/false): ");
            if (bool.TryParse(Console.ReadLine(), out remote)) break;
            Console.WriteLine("Enter true or false.");
        }

        return new Employee { Department = dep, Salary = salary, RemoteWork = remote };
    }
}