    namespace Battleship
    {
        using System;
        using System.Collections.ObjectModel;
        using System.Drawing;
    
        public class RandomOpponent : IBattleshipOpponent
        {
            Random rand = new Random();
            Version version = new Version(1, 0);
            Size gameSize;
    
            public string Name { get { return "Random"; } }
    
            public Version Version { get { return this.version; } }
    
            public void NewGame(Size size, TimeSpan timeSpan) { this.gameSize = size; }
    
            public void PlaceShips(ReadOnlyCollection<Ship> ships)
            {
                foreach (Ship s in ships)
                {
                    s.Place(
                        new Point(
                            (int)(rand.NextDouble() * this.gameSize.Width),
                            (int)(rand.NextDouble() * this.gameSize.Height)),
                        (ShipOrientation)(int)(rand.NextDouble() * 2));
                }
            }
    
            public Point GetShot()
            {
                return new Point(
                    (int)(rand.NextDouble() * this.gameSize.Width),
                    (int)(rand.NextDouble() * this.gameSize.Height));
            }
    
            public void OpponentShot(Point shot) { }
    
            public void ShotHit(Point shot, bool sunk) { }
    
            public void ShotMiss(Point shot) { }
        }
    }
