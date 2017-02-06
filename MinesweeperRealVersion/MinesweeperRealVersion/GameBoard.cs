using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperRealVersion
{
    public class GameBoard
    {
        //for all intents and purposes the name of the board should not be the same as another board
        public string Name { get; set; }
        public readonly int SizeX;
        public readonly int SizeY;
        

        public enum Difficulty
        {
            Easy,
            Medium,
            Hard
        }

        //for like EasyBoard, MediumBoard, HardBoard
        protected Difficulty difficulty;
        //perhaps something like 3,4,5 bombs per 3x3 block of tiles

        public Tile[,] tiles;
        public List<BombTile> bombTiles;
        //it's good to note that x and y are flipped in a 2d array    

        public GameBoard(int sizeX, int sizeY, Difficulty difficulty)
        {
            this.difficulty = difficulty;
            bombTiles = new List<BombTile>();
            SizeX = sizeX;
            SizeY = sizeY;
            tiles = new Tile[sizeY, sizeX];
        }

        private void GenerateBlankTiles()
        {
            for(int i = 0; i < SizeY; i++)
            {
                for(int j = 0; j < SizeX; j++)
                {
                    tiles[j, i] = new EmptyTile(j, i);
                }
            }
        }

        //bomb this shit up!
        private void PutBombsInRandomTiles()
        {
            //hmm...how do i do this, do i like do the determinant method or do i just pick random tiles...
        }

        private void UpdateTilesToMatchBombs()
        {

        }

        private void UpdateBoard()
        {
            for(int i = 0; i < SizeY; i++)
            {
                for(int j = 0; j < SizeX; j++)
                {

                    //TODO, figure out best way of transforming tiles with adjacent bombs into numbertiles.
                    if (!(tiles[i,j] is BombTile))
                    {
                        int amt = 0;
                        GetAdjacentBombs(tiles[i, j], out amt);
                        if (amt != 0)
                        {
                            tiles[i, j] = (ClickedTileWithNumber)tiles[i, j];
                            tiles[i, j].CallThisOnMeOnceEverythingIsFinished(this);
                        }
                        else
                        {
                            tiles[i, j].CallThisOnMeOnceEverythingIsFinished(this);
                        }
                    }
                }
            }
        }

        public void GenerateTest()
        {
            Random rand = new Random();
            for(int i = 0; i < SizeX; i++)
            {
                for(int j = 0; j < SizeY; j++)
                {
                    tiles[j, i] = new BombTile(j,i);
                }
            }

            for(int i = 0; i < SizeX; i+=3)
            {
                for (int j = 0; j < SizeY; j += 2)
                {
                    tiles[j, i] = new ClickedTileWithNumber(j, i,rand.Next(0,8));
                }
            }
        }

        public override string ToString()
        {
            string ret = "";
            for(int i = 0; i < SizeY; i++)
            {
                ret += "| ";
                for(int j = 0; j < SizeX; j++)
                {
                    if (tiles[j, i] is BombTile) ret += "X |";
                    else if (tiles[j, i] is ClickedTileWithNumber) ret += $"{((ClickedTileWithNumber)tiles[j, i]).AmountOfAdjacentBombs} |";
                    else ret += "  |";
                }
                ret += "\n";
            }
            return ret;
        }

        public void GetAllBombTiles()
        {
            foreach (Tile tile in tiles)
            {
                if (tile is BombTile)
                {
                    bombTiles.Add((BombTile)tile);
                }
            }    
        }

        public List<Tile> GetAdjacentBombsByIndex(int x, int y, out int amount)
        {
            return GetAdjacentBombs(tiles[y, x], out amount);
        }

        public List<Tile> GetAdjacentBombs(Tile tile, out int amount)
        {
            int x = tile.X;
            int y = tile.Y;
            int total = 0;
            //on second thought, probably doing it manually would be better
            List<Tile> bombs = new List<Tile>();
            Tuple<int, int>[] positions = new Tuple<int, int>[]
            {
                new Tuple<int, int>(x-1,y-1), //bottom left
                new Tuple<int, int>(x,y-1), //bottom middle
                new Tuple<int, int>(x+1,y-1), //bottom right
                new Tuple<int, int>(x-1,y), //middle left
                new Tuple<int, int>(x+1,y), //middle right
                new Tuple<int, int>(x-1,y+1), //top left
                new Tuple<int, int>(x,y+1), //top middle
                new Tuple<int,int>(x+1,y+1) //top right
            };
            foreach(Tuple<int,int> position in positions)
            {
                try
                {
                    if (tiles[position.Item2,position.Item1] is BombTile)
                    {
                        total++;
                        bombs.Add(tiles[position.Item2, position.Item1]);
                    }
                }
                catch(IndexOutOfRangeException e)
                {

                }
            }
            amount = total;
            return bombs;
            
            
            

        }

        public static bool HasSameLocation(Tile one, Tile two)
        {
            return one.X == two.X && one.Y == two.Y;
        }

        public static bool IsSameType(Tile one, Tile two)
        {
            return one.GetType().Equals(two.GetType());
        }

        public static bool HasSameGameBoard(Tile one, Tile two)
        {
            return one.Board.Equals(two.Board);
        }

        public static bool IsBasicallyTheSameTile(Tile one, Tile two)
        {
            return HasSameLocation(one, two) && IsSameType(one, two) && HasSameGameBoard(one, two);
        }

        public bool Equals(GameBoard other)
        {
            return Name == other.Name;
        }
    }
}
