// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;

public class TaskItem
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int Priority { get; set; }

    public override string ToString()
    {
        return $"[Priority {Priority}] {Title} - {Description}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        string filePath = "tasks.json";
        List<TaskItem> tasks = LoadTasks(filePath);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("==== Task Manager ====");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. Show Tasks");
            Console.WriteLine("3. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine()!;

            switch (choice)
            {
                case "1":
                    AddTask(tasks, filePath);
                    break;
                case "2":
                    ShowTasks(tasks);
                    break;
                case "3":
                    Console.WriteLine("Exiting...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    static void AddTask(List<TaskItem> tasks, string filePath)
    {
        Console.Write("Task Title: ");
        string title = Console.ReadLine()!;

        Console.Write("Description: ");
        string description = Console.ReadLine()!;

        Console.Write("Priority (1-5): ");
        int priority = int.Parse(Console.ReadLine()!);

        tasks.Add(new TaskItem
        {
            Title = title,
            Description = description,
            Priority = priority
        });

        SaveTasks(tasks, filePath);
        Console.WriteLine("Task added and saved.");
    }

    static void ShowTasks(List<TaskItem> tasks)
    {
        Console.WriteLine("\n==== Task List ====");
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks found.");
            return;
        }

        foreach (var task in tasks.OrderBy(t => t.Priority))
        {
            Console.WriteLine(task);
        }
    }

    static void SaveTasks(List<TaskItem> tasks, string filePath)
    {
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    static List<TaskItem> LoadTasks(string filePath)
    {
        if (!File.Exists(filePath))
            return new List<TaskItem>();

        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
    }
}
