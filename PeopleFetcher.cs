using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CatWorx.BadgeMaker
{
  class PeopleFetcher
  {
    public static List<Employee> GetEmployees()
    {
       List<Employee> employees = new List<Employee>();
      // Collect user values until the value is an empty string
      while (true)
      {
        Console.WriteLine("Please enter a name (leave empty to exit): ");
        // Get a name from the console and assign it to a variable
        string input = Console.ReadLine() ?? "";
        // Break if the user hits enter without typing a name
        if (input == "")
        {
          break;
        }

        // add the other values
        Console.WriteLine("Enter last name: ");
        string lastName = Console.ReadLine() ?? "";
        Console.WriteLine("Enter ID: ");
        int id = Int32.Parse(Console.ReadLine() ?? "");
        Console.WriteLine("Enter Photo URL: ");
        string photoUrl = Console.ReadLine() ?? "";

        // Create a new Employee instance
        Employee currentEmployee = new Employee(input, lastName, id, photoUrl);
        employees.Add(currentEmployee);
      }
      return employees;
    }

    async public static Task<List<Employee>> GetFromApi()
    {
      List<Employee> employees = new List<Employee>();

      using (HttpClient client = new HttpClient())
      {
        string response = await client.GetStringAsync("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture");
        JObject json = JObject.Parse(response);
        List<JToken> results = json["results"]!.Children().ToList();

        foreach (JToken result in results)
        {
          Employee emp = new Employee
          (
            result["name"]!["first"]!.ToString(),
            result["name"]!["last"]!.ToString(),
            Int32.Parse(result["id"]!["value"]!.ToString().Replace("-", "")),
            result["picture"]!["large"]!.ToString()
          );

          employees.Add(emp);
        }
      }

      return employees;
    }
  }
}