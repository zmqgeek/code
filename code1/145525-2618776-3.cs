    public class AlienMonster : IMove, IAct
    {
      private List<IAbility> _abilities;
      private List<ILocomotion> _locomotions;
    
      public AlienMonster()
      {
        _abilities = new List<IAbility>() {new Growl()};
        _locomotion = new List<ILocomotion>() {new Walk(), new Run(), new Swim()}
      }
      public void Move()
      {
        // implementation for the IMove interface
      }
    
      public void Act()
      {
        // implementation for the IAct interface
      }
    
    }
