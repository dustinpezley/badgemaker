using System.Globalization;

namespace CatWorx.BadgeMaker{
  class Employee
  {
    private string FirstName;
    private string LastName;
    private int Id;
    private string PhotoUrl;

    public Employee(string firstName, string lastName, int id, string photoUrl) {
      // Set TextInfo to convert names to Title Case
      TextInfo myTI = new CultureInfo("en-US", false).TextInfo;

      FirstName = myTI.ToTitleCase(firstName);
      LastName = myTI.ToTitleCase(lastName);
      Id = id;
      PhotoUrl = photoUrl;
    }

    public string GetFullName() {
      return FirstName + " " + LastName;
    }

    public int GetId() {
      return Id;
    }

    public string GetPhotoUrl() {
      return PhotoUrl;
    }

    public string GetCompanyName() {
      return "Cat Worx";
    }
  }
}