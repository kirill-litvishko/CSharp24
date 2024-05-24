using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;

class Program
{
    static void Main()
    {
        string xmlFpath = "employees.xml";
        XDocument xmlDoc = XDocument.Load(xmlFpath);

        var employees = xmlDoc.Root.Elements("Employee")
            .Select(e => new Employee
            {
                Name = e.Element("Name").Value,
                Position = e.Element("Position").Value,
                HireDate = DateTime.Parse(e.Element("HireDate").Value)
            }).ToList();

        var sortedEmployees = employees.OrderBy(e => e.HireDate).ToList();

        string sorterXmlFpath = "sorted_employees.xml";
        var sortedXml = new XElement("Employees",
            sortedEmployees.Select(e =>
                new XElement("Employee",
                    new XElement("Name", e.Name),
                    new XElement("Position", e.Position,
                    new XElement("HireDate", e.HireDate.ToString("yyyy-MM-dd"))
                )
            )
        )
    );

        sortedXml.Save(sorterXmlFpath);

        string TxtFpath = "employees.txt";
        using (StreamWriter writer = new StreamWriter(TxtFpath))
        {
            foreach (var employee in sortedEmployees)
            {
                writer.WriteLine($"Name: {employee.Name} Position: {employee.Position} HireDate: {employee.HireDate:yyyy-MM-dd}");
            }
        }

        Console.WriteLine("Contents of sorted_employees.xml:\n");
        Console.WriteLine(File.ReadAllText(sorterXmlFpath));

        Console.WriteLine("\nContents of employees.txt:\n");
        Console.WriteLine(File.ReadAllText(TxtFpath));
    }
}

class Employee
{
    public string Name { get; set; }
    public string Position { get; set; }
    public DateTime HireDate { get; set; }
}

