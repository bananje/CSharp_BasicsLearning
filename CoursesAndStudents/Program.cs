// See https://aka.ms/new-console-template for more information

using CoursesAndStudents;
using Microsoft.EntityFrameworkCore;
using static System.Console;

using (Academy db = new())
{
    bool deleted = await db.Database.EnsureDeletedAsync();
    WriteLine($"Database deleted: {deleted}");

    bool created = await db.Database.EnsureCreatedAsync();
    WriteLine($"Database created: {created}");

    WriteLine("SQL script used to create database:");
    WriteLine(db.Database.GenerateCreateScript());

    foreach (Student s in db.Students.Include(s => s.Courses))
    {
        WriteLine("{0} {1} attends the following {2} courses:",
        s.FirstName, s.LastName, s.Courses.Count);
        foreach (Course c in s.Courses)
        {
            WriteLine($" {c.Title}");
        }
    }
}