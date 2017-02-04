using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperRealVersion
{
    public class GameBoard
    {
        public readonly int SizeX;

        public readonly int SizeY;

        public Tile[,] tiles;
        public List<BombTile> bombTiles;
        //it's good to note that x and y are flipped in a 2d array    

        public GameBoard(int sizeX, int sizeY)
        {
            bombTiles = new List<BombTile>();
            SizeX = sizeX;
            SizeY = sizeY;
            tiles = new Tile[sizeY, sizeX];
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

        public bool HasSameLocation(Tile one, Tile two)
        {
            return one.X == two.X && one.Y == two.Y;
        }

        public bool IsSameType(Tile one, Tile two)
        {
            return one.GetType().Equals(two.GetType());
        }

        public bool IsBasicallyTheSameTile(Tile one, Tile two)
        {
            return HasSameLocation(one, two) && IsSameType(one, two);
        }
    }
}
