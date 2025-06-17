using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
public class Node
{
    public Employee Data { get; set; }
    public Node Next { get; set; }
    public Node Previous { get; set; }

    public Node(Employee data)
    {
        Data = data;
    }
}
public class Employee
{
    public Department Department { get; set; }
    public double Salary { get; set; }
    public bool RemoteWork { get; set; }
}

public enum Department
{
    HR, IT, Marketing, Sales, Finance
}



public class EmployeeList
{
    private Node head = null;
    private Node tail = null;
    private int count = 0;

    public int Length => count;

    public void AddToEnd(Employee employee)
    {
        var node = new Node(employee);
        if (head == null)
        {
            head = tail = node;
        }
        else
        {
            tail!.Next = node;
            node.Previous = tail;
            tail = node;
        }
        count++;
    }

    public void RemoveFromStart()
    {
        if (head == null) return;

        head = head.Next;
        if (head != null) head.Previous = null;
        else tail = null;
        count--;
    }

    public Employee? GetAt(int index)
    {
        if (index < 0 || index >= count) return null;

        var current = head;
        for (int i = 0; i < index; i++)
            current = current!.Next;

        return current!.Data;
    }

    public bool SetAt(int index, Employee employee)
    {
        if (index < 0 || index >= count) return false;

        var current = head;
        for (int i = 0; i < index; i++)
            current = current!.Next;

        current!.Data = employee;
        return true;
    }

    public double AverageSalary()
    {
        if (count == 0) return 0;
        double sum = 0;
        var current = head;
        while (current != null)
        {
            sum += current.Data.Salary;
            current = current.Next;
        }
        return sum / count;
    }

    public void PrintTable()
    {
        Console.WriteLine($"\n{"Index",-6} {"Department",-12} {"Salary",-10} {"Remote",-8}");
        Console.WriteLine(new string('-', 40));
        var current = head;
        int i = 0;
        while (current != null)
        {
            var e = current.Data;
            Console.WriteLine($"{i++,6} {e.Department,-12} {e.Salary,-10:F2} {e.RemoteWork,-8}");
            current = current.Next;
        }
    }

    public void SaveToFile(string path)
    {
        var list = new List<Employee>();
        var current = head;
        while (current != null)
        {
            list.Add(current.Data);
            current = current.Next;
        }
        File.WriteAllText(path, JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true }));
        Console.WriteLine("Saved to file.");
    }

    public void LoadFromFile(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("File not found.");
            return;
        }

        var json = File.ReadAllText(path);
        var list = JsonSerializer.Deserialize<List<Employee>>(json);
        if (list != null)
        {
            head = tail = null;
            count = 0;
            foreach (var e in list)
                AddToEnd(e);
            Console.WriteLine("Loaded from file.");
        }
    }

    public void SearchCriteria()
    {
        double avg = AverageSalary();
        bool found = false;
        var current = head;

        Console.WriteLine("\nRemote employees with salary below average:");
        Console.WriteLine(new string('-', 40));

        while (current != null)
        {
            var e = current.Data;
            if (e.RemoteWork && e.Salary < avg)
            {
                Console.WriteLine($"{e.Department,-12} {e.Salary,-10:F2} Remote: {e.RemoteWork}");
                found = true;
            }
            current = current.Next;
        }

        if (!found)
            Console.WriteLine("None found.");
    }
}
