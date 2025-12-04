using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

public class Group
{
    public string Name { get; set; }
    public int StudentCount { get; set; }
    public string Specialty { get; set; }

    public override string ToString() => $"Name: {Name}, Student Count: {StudentCount}, Specialty: {Specialty}";
}

class Program
{
    static void Main()
    {
        var groups = new List<Group>
        {
            new Group { Name = "CS-101", StudentCount = 25, Specialty = "Computer Science" },
            new Group { Name = "IT-202", StudentCount = 30, Specialty = "Information Technology" },
            new Group { Name = "SE-303", StudentCount = 20, Specialty = "Software Engineering" }
        };

        string jsonFile = "groups.json";
        string jsonData = JsonSerializer.Serialize(groups, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonFile, jsonData);

        var deserializedGroups = JsonSerializer.Deserialize<List<Group>>(File.ReadAllText(jsonFile));

        Console.WriteLine("JSON format of all groups:");
        if (deserializedGroups != null)
        {
            foreach (var group in deserializedGroups)
            {
                Console.WriteLine(group);
            }
        }
    }
}
