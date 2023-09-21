// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;

namespace CatWorx.BadgeMaker
{
  class Program
  {
    static void Main(string[] args)
    {
      List<string> employees = new List<string>() { "adam", "amy" };
      employees.Add("barbara");
      employees.Add("billy");
      TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
      Console.WriteLine("Employees");
      Console.WriteLine("---------");
      foreach (string employee in employees)
      {
        Console.WriteLine(myTI.ToTitleCase(employee));
      }
    }
  }
}
