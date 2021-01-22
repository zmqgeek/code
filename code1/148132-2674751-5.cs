    using System;    
    [Flags] public enum PetType
    {
       None = 0, Dog = 1, Cat = 2, Rodent = 4, Bird = 8, Reptile = 16, Other = 32
    };
    
    public class Example
    {
       public static void Main()
       {
          object value;     
          // Call IsDefined with underlying integral value of member.
          value = 1;
          Console.WriteLine("{0}: {1}", value, Enum.IsDefined(typeof(PetType), value));
          // Call IsDefined with invalid underlying integral value.
          value = 64;
          Console.WriteLine("{0}: {1}", value, Enum.IsDefined(typeof(PetType), value));
          // Call IsDefined with string containing member name.
          value = "Rodent";
          Console.WriteLine("{0}: {1}", value, Enum.IsDefined(typeof(PetType), value));
          // Call IsDefined with a variable of type PetType.
          value = PetType.Dog;
          Console.WriteLine("{0}: {1}", value, Enum.IsDefined(typeof(PetType), value));
          value = PetType.Dog | PetType.Cat;
          Console.WriteLine("{0}: {1}", value, Enum.IsDefined(typeof(PetType), value));
          // Call IsDefined with uppercase member name.      
          value = "None";
          Console.WriteLine("{0}: {1}", value, Enum.IsDefined(typeof(PetType), value));
          value = "NONE";
          Console.WriteLine("{0}: {1}", value, Enum.IsDefined(typeof(PetType), value));
          // Call IsDefined with combined value
          value = PetType.Dog | PetType.Bird;
          Console.WriteLine("{0:D}: {1}", value, Enum.IsDefined(typeof(PetType), value));
          value = value.ToString();
          Console.WriteLine("{0:D}: {1}", value, Enum.IsDefined(typeof(PetType), value));
       }
    }
