using System.Globalization;

namespace CatWorx.BadgeMaker{
  class Employee
  {
    public string FirstName;
    public string LastName;
    public int Id;
    public string PhotoUrl;

    public Employee(string firstName, string lastName) {
      // Set TextInfo to convert names to Title Case
      TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

      FirstName = myTI.ToTitleCase(firstName);
      LastName = myTI.ToTitleCase(lastName);
    }

    public string GetFullName() {
      return FirstName + " " + LastName;
    }
  }
}