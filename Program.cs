// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Net;
using System.Runtime.Serialization;



namespace CatWorx.BadgeMaker
{
  class Program
  {
    async static Task Main(string[] args)
    {
      List<Employee> employees;
      ConsoleKey response;
      do
      {
        Console.Write("Would you like to fetch existing employees? [y/n]");
        response = Console.ReadKey(false).Key;
        if (response != ConsoleKey.Enter)
        {
          Console.WriteLine();
        }
      } while (response != ConsoleKey.Y && response != ConsoleKey.N);

      if (response == ConsoleKey.Y)
      {
        employees = await PeopleFetcher.GetFromApi();
      } else
      {
        employees = PeopleFetcher.GetEmployees();
      }
      
      Util.PrintEmployees(employees);
      Util.MakeCSV(employees);
      await Util.MakeBadges(employees);
    }
  }
}
