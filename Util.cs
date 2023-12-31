using System;
using System.Collections.Generic;
using System.IO;
using SkiaSharp;
using System.Net.Http;
using System.Threading.Tasks;

namespace CatWorx.BadgeMaker
{
  class Util
  {
    public static void PrintEmployees(List<Employee> employees)
    {
      // Write employee list to console
      Console.WriteLine("                 Employees");
      Console.WriteLine("----------------------------------------------------");
      for (int i = 0; i < employees.Count; i++)
      {
        string template = "{0,-10}\t{1,-20}\t{2}";
        Console.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetFullName(), employees[i].GetPhotoUrl()));
      }
    }

    public static void MakeCSV(List<Employee> employees)
    {
      if (!Directory.Exists("data"))
      {
        Directory.CreateDirectory("data");
      }

      using (StreamWriter file = new StreamWriter("data/employees.csv"))
      {
        file.WriteLine("ID,Name,PhotoURL");

        foreach (Employee employee in employees)
        {
          string template = "{0},{1},{2}";
          file.WriteLine(String.Format(template, employee.GetId(), employee.GetFullName(), employee.GetPhotoUrl()));
        }
      }
    }

    async public static Task MakeBadges(List<Employee> employees)
    {
      // Layout variables
      int BADGE_WIDTH = 669;
      int BADGE_HEIGHT = 1044;

      int PHOTO_LEFT_X = 184;
      int PHOTO_TOP_Y = 215;
      int PHOTO_RIGHT_X = 486;
      int PHOTO_BOTTOM_Y = 517;

      int COMPANY_NAME_Y = 150;

      int EMPLOYEE_NAME_Y = 600;

      int EMPLOYEE_ID_Y = 730;

      using (HttpClient client = new HttpClient())
      {
        foreach (Employee employee in employees)
        {

          SKPaint paint = new SKPaint();
          paint.TextSize = 42.0f;
          paint.IsAntialias = true;
          paint.Color = SKColors.White;
          paint.IsStroke = false;
          paint.TextAlign = SKTextAlign.Center;
          paint.Typeface = SKTypeface.FromFamilyName("Arial");

          SKImage photo = SKImage.FromEncodedData(await client.GetStreamAsync(employee.GetPhotoUrl()));
          SKImage background = SKImage.FromEncodedData(File.OpenRead("badge.png"));

          SKBitmap badge = new SKBitmap(BADGE_WIDTH, BADGE_HEIGHT);

          SKCanvas canvas = new SKCanvas(badge);

          canvas.DrawImage(background, new SKRect(0, 0, BADGE_WIDTH, BADGE_HEIGHT));
          canvas.DrawImage(photo, new SKRect(PHOTO_LEFT_X, PHOTO_TOP_Y, PHOTO_RIGHT_X, PHOTO_BOTTOM_Y));

          // Company name
          canvas.DrawText(employee.GetCompanyName(), BADGE_WIDTH / 2f, COMPANY_NAME_Y, paint);

          // Employee name
          paint.Color = SKColors.Black;
          canvas.DrawText(employee.GetFullName(), BADGE_WIDTH / 2f, EMPLOYEE_NAME_Y, paint);

          // Employee ID
          paint.Typeface = SKTypeface.FromFamilyName("Courier New");
          canvas.DrawText(employee.GetId().ToString(), BADGE_WIDTH / 2f, EMPLOYEE_ID_Y, paint);

          SKImage finalImage = SKImage.FromBitmap(badge);
          SKData data = finalImage.Encode();
          data.SaveTo(File.OpenWrite($"data/{employee.GetId()}Badge.png"));
          // SKData data = background.Encode();
          // data.SaveTo(File.OpenWrite("data/employeeBadge.png"));
        }
      }
    }
  } 
}