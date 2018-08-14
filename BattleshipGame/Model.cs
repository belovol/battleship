using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame
{
    class Model
    {
        public BattleField playerField;
        public BattleField AIField;

        Random rnd = new Random();

        public Model()
        {
            playerField = new PlayerField();
            AIField = new AIField();
        }

        List<Cell> possibleCells = new List<Cell>();

        void AddPossibleCell(int x, int y)
        {
            if (x < 0 || x >= Globals.fieldSize ||
                y < 0 || y >= Globals.fieldSize)
            {
                return;
            }
            var cell = playerField.Field[y, x];
            if (cell.shotStatus == Cell.ShotStatus.none)
                possibleCells.Add(cell);
        }

        int lastX, lastY;
        Cell.TurnResult lastResult;
        public void AITurn()
        {
            if (possibleCells.Count == 0)
            {
                int x, y;
                Cell.TurnResult result;
                do
                {
                    x = rnd.Next(Globals.fieldSize);
                    y = rnd.Next(Globals.fieldSize);

                    var cell = playerField.Field[y, x];

                    result = cell.handleShot();
                }
                while (result == Cell.TurnResult.fail);
                
                if (result == Cell.TurnResult.hit)
                {
                    lastX = x;
                    lastY = y;
                    TryPredict();
                }

                lastResult = result;
            }
            else
            {
                var cell = possibleCells[0];
                var result = cell.handleShot();
                lastResult = result;

                if (result == Cell.TurnResult.miss)
                {
                    possibleCells.RemoveAt(0);
                }
                else if (result == Cell.TurnResult.hit && lastResult == Cell.TurnResult.hit)
                {
                    possibleCells = new List<Cell>();
                    int deltaX = lastX - cell.x;
                    int deltaY = lastY - cell.y;

                    if (deltaX != 0)
                    {
                        int x1 = Math.Min(cell.x, lastX) - 1;
                        int x2 = Math.Max(cell.x, lastX) + 1;
                        AddPossibleCell(x1, cell.y);
                        AddPossibleCell(x2, cell.y);
                    }

                    if (deltaY != 0)
                    {
                        int y1 = Math.Min(cell.y, lastY) - 1;
                        int y2 = Math.Max(cell.y, lastY) + 1;
                        AddPossibleCell(cell.x, y1);
                        AddPossibleCell(cell.x, y2);
                    }
                    
                }
                else if (result == Cell.TurnResult.kill)
                {
                    lastResult = Cell.TurnResult.kill;
                    possibleCells = new List<Cell>();
                }
            }

        }

        private void TryPredict()
        {
            Tuple<int, int>[] deltas = { Tuple.Create(-1, 0), Tuple.Create(1, 0),
                                         Tuple.Create(0, -1), Tuple.Create(0, 1)};

            foreach (var delta in deltas)
            {
                AddPossibleCell(lastX + delta.Item1, lastY + delta.Item2);
            }


        }

    }

    public static class Globals
    {
        public static readonly int cellSize = 40;
        public static readonly int fieldSize = 10;
    }

    abstract class BattleField
    {
        public Cell[,] Field;
        public List<Ship> ships;


        public Bitmap bmp;
        public readonly Graphics g;
        protected Random rnd;
        protected bool ShowShips = true;

        public bool drawShips = false;

        public BattleField()
        {
            Field = new Cell[Globals.fieldSize, Globals.fieldSize];

            for (int i = 0; i < Globals.fieldSize; i++)
            {
                for (int j = 0; j < Globals.fieldSize; j++)
                {
                    Field[i, j] = new Cell(j, i, this);
                }
            }

            ships = new List<Ship>();

            bmp = new Bitmap(Globals.fieldSize * Globals.cellSize, Globals.fieldSize * Globals.cellSize);
            g = Graphics.FromImage(bmp);
            rnd = new Random(this.GetHashCode());
            //PossibleToPlaceShipCheck(4);
        }
        
        public void GenerateBitmap(Ship.Direction direction = Ship.Direction.horizontal, bool showForbiddenCells = false)
        {
            //заливаем поле фоновым цветом
            var brush = new SolidBrush(Color.White);
            g.FillRectangle(brush, 0, 0, Globals.fieldSize * Globals.cellSize, Globals.fieldSize * Globals.cellSize);

            //рисуем клетки
            Color color = Color.White;
            for (int i = 0; i < Globals.fieldSize; i++)
            {
                for (int j = 0; j < Globals.fieldSize; j++)
                {
                    var cell = Field[i, j];

                    if (cell.status == Cell.Status.ship && (ShowShips || cell.shotStatus == Cell.ShotStatus.damagedShip))
                    {
                        if (cell.ship.isAlive)
                            color = Color.Black;
                        else
                            color = Color.DarkViolet;
                    }                    
                    else if (ShowShips && cell.status == Cell.Status.nearShip) color = Color.Linen;
                    else if (showForbiddenCells &&
                             (direction == Ship.Direction.horizontal && !cell.possibleToPlaceShipH ||
                              direction == Ship.Direction.vertical && !cell.possibleToPlaceShipV)
                            )
                        color = Color.Pink;
                    else
                        color = Color.White;

                    brush = new SolidBrush(color);
                    int x1 = cell.x * Globals.cellSize;
                    int y1 = cell.y * Globals.cellSize;
                    g.FillRectangle(brush, x1, y1, Globals.cellSize, Globals.cellSize);

                    //рисуем результат выстрелов
                    if (cell.shotStatus == Cell.ShotStatus.miss)
                    {
                        brush = new SolidBrush(Color.Blue);
                        int x = x1 + Globals.cellSize * 2 / 5;
                        int y = y1 + Globals.cellSize * 2 / 5;
                        int size = Globals.cellSize / 5;
                        g.FillEllipse(brush, x, y, size, size);
                    }

                    if (cell.shotStatus == Cell.ShotStatus.damagedShip)
                    {
                        var p = new Pen(Color.Red);
                        p.Width = 2;
                        g.DrawLine(p, x1, y1, x1 + Globals.cellSize, y1 + Globals.cellSize);
                        g.DrawLine(p, x1, y1 + Globals.cellSize, x1 + Globals.cellSize, y1);
                    }
                }
            }

            //рисуем сетку
            Pen pen = new Pen(Color.Black);
            for (int i = 1; i < Globals.fieldSize; i++)
            {
                g.DrawLine(pen, i * Globals.cellSize, 0, i * Globals.cellSize, Globals.fieldSize * Globals.cellSize);

            }

            for (int i = 1; i < Globals.fieldSize; i++)
            {
                g.DrawLine(pen, 0, i * Globals.cellSize, Globals.fieldSize * Globals.cellSize, i * Globals.cellSize);

            }
        }

        public void PossibleToPlaceShipCheck(int size) // проверяем возможность поставить корабль для каждой клетки
        {
            for (int i = 0; i < Globals.fieldSize; i++)
            {
                for (int j = 0; j < Globals.fieldSize; j++)
                {
                    Field[i, j].PossibleToPlaceShipCheck(size);
                }
            }
        }

        private void PlaceSquadRandomly(int size, int amount)
        {
            for (int n = 0; n < amount; n++)
            {
                var places = new List<Place>(); //список возможных положений корабля
                PossibleToPlaceShipCheck(size);
                Place place;
                for (int i = 0; i < Globals.fieldSize; i++)
                {
                    for (int j = 0; j < Globals.fieldSize; j++)
                    {
                        if (Field[i, j].possibleToPlaceShipH)
                        {
                            place = new Place
                            {
                                direction = Ship.Direction.horizontal,
                                start = Field[i, j]
                            };
                            places.Add(place);
                        }
                        if (Field[i, j].possibleToPlaceShipV)
                        {
                            place = new Place
                            {
                                direction = Ship.Direction.vertical,
                                start = Field[i, j]
                            };
                            places.Add(place);
                        }

                    }
                }
                if (places.Count == 0)
                {
                    Console.Write("bl");
                }
                else
                {
                    int r = rnd.Next(places.Count);
                    place = places[r];
                    PlaceShip(place.start, size, place.direction);
                }

            }
        }
        struct Place
        {
            public Ship.Direction direction;
            public Cell start;

        }
        public void PlaceShipsRandomly()
        {
            //размещаем 4 - палубный
            PlaceSquadRandomly(4, 1);

            //3 - палубные
            PlaceSquadRandomly(3, 2);

            //2 - палубный
            PlaceSquadRandomly(2, 3);

            //1 - палубный
            PlaceSquadRandomly(1, 4);
        }

        public bool PlaceShip(Cell start, int size, Ship.Direction direction)
        {

            //если нельзя поставить, выходим
            if (direction == Ship.Direction.horizontal && !start.possibleToPlaceShipH)
                return false;
            if (direction == Ship.Direction.vertical && !start.possibleToPlaceShipV)
                return false;
            
            var ship = new Ship(size);

            //обновлем поле
            if (direction == Ship.Direction.horizontal)
            {
                for (int i = 0; i < size; i++)
                {
                    Field[start.y, start.x + i].status = Cell.Status.ship;
                    Field[start.y, start.x + i].ship = ship;

                    if (start.y + 1 < Globals.fieldSize)
                    {
                        ship.nearCells.Add(Field[start.y + 1, start.x + i]);
                    }

                    if (start.y - 1 >= 0)
                    {
                        ship.nearCells.Add(Field[start.y - 1, start.x + i]);
                    }
                }

                Tuple<int, int>[] deltas = { Tuple.Create(-1, -1), Tuple.Create(size, -1),
                                             Tuple.Create(-1, 0),  Tuple.Create(size, 0),
                                             Tuple.Create(-1, 1),  Tuple.Create(size, 1) };
                foreach (var delta in deltas)
                {
                    if (start.x + delta.Item1 < 0 || start.x + delta.Item1 >= Globals.fieldSize ||
                        start.y + delta.Item2 < 0 || start.y + delta.Item2 >= Globals.fieldSize)
                        continue;

                    ship.nearCells.Add(Field[start.y + delta.Item2, start.x + delta.Item1]);
                }

            }
            else if (direction == Ship.Direction.vertical)
            {
                for (int i = 0; i < size; i++)
                {
                    Field[start.y + i, start.x].status = Cell.Status.ship;
                    Field[start.y + i, start.x].ship = ship;

                    if (start.x + 1 < Globals.fieldSize)
                    {
                        ship.nearCells.Add(Field[start.y + i, start.x + 1]);
                    }

                    if (start.x - 1 >= 0)
                    {
                        ship.nearCells.Add(Field[start.y + i, start.x - 1]);
                    }
                }

                
                Tuple<int, int>[] deltas = { Tuple.Create(-1, -1), Tuple.Create(0, -1), Tuple.Create(1, -1),
                                             Tuple.Create(-1, size),  Tuple.Create(0, size),  Tuple.Create(1, size) };

                foreach (var delta in deltas)
                {
                    if (start.x + delta.Item1 < 0 || start.x + delta.Item1 >= Globals.fieldSize ||
                        start.y + delta.Item2 < 0 || start.y + delta.Item2 >= Globals.fieldSize)
                        continue;

                    ship.nearCells.Add(Field[start.y + delta.Item2, start.x + delta.Item1]);
                }
                
            }

            foreach (var cell in ship.nearCells)
            {
                cell.status = Cell.Status.nearShip;
            }

            ships.Add(ship);
            return true;
        }

        public int CountAliveShips()
        {
            int res = 0;
            foreach (var ship in ships)
            {
                if (ship.isAlive)
                    res++;
            }
            return res;
        }
    }

    class PlayerField : BattleField
    {
        public PlayerField()
        {
            drawShips = true;
        }
    }
    class AIField : BattleField
    {

        public AIField()
        {
            PlaceShipsRandomly();
            ShowShips = false;
        }

    }

    class Cell
    {
        public enum Status { ship, nearShip, free }
        public enum ShotStatus { miss, damagedShip, none }

        public enum TurnResult { fail, miss, hit, kill}

        public Status status = Status.free;
        public ShotStatus shotStatus = ShotStatus.none;

        public Ship ship = null; //ссылка на корабль в клетке

        public bool possibleToPlaceShipH = true;
        public bool possibleToPlaceShipV = true;

        BattleField parent;

        public readonly int x, y;

        public Cell(int x, int y, BattleField field)
        {
            this.x = x;
            this.y = y;
            this.parent = field;
        }

        public TurnResult handleShot()
        {
            if (shotStatus != ShotStatus.none)
                return TurnResult.fail;
          

            if (ship != null)
            {
                var result = ship.HandleShot();
                shotStatus = ShotStatus.damagedShip;
                if (result == Ship.ShotResult.killed)
                    return TurnResult.kill;
                else
                    return TurnResult.hit;
            }
            else
            {
                shotStatus = ShotStatus.miss;
                return TurnResult.miss;
            }


        }

        public bool PossibleToPlaceShipCheck(int size)
        {
            possibleToPlaceShipH = true;
            possibleToPlaceShipV = true;

            if (x == 9 && y == 6)
            {
                Console.Write("bl");
            }

            for (int i = 0; i < size; i++)
            {
                if ( x + i >= Globals.fieldSize || parent.Field[y, x + i].status != Status.free)
                {
                    possibleToPlaceShipH = false;
                    break;
                }
            }

            for (int i = 0; i < size; i++)
            {
                if (y + i >= Globals.fieldSize || parent.Field[y + i, x].status != Status.free)
                {
                    possibleToPlaceShipV = false;
                    break;
                }
            }

            return possibleToPlaceShipH || possibleToPlaceShipV;
        }

    }

    class Ship
    {
        public enum Direction { horizontal, vertical}
        public enum ShotResult { hit, killed }

        public readonly int size;

        public List<Cell> nearCells = new List<Cell>();
        public int hp { get; private set; }
        public bool isAlive { get; private set; }
        
        public Ship(int size)
        {
            this.size = size;
            hp = size;
            isAlive = true;
        }

        public ShotResult HandleShot()
        {
            if (hp > 1)
            {
                hp--;
                return ShotResult.hit;
            }
            else
            {
                hp = 0;
                Die();
                return ShotResult.killed;
            }

        }

        public void Die()
        {
            isAlive = false;
            foreach (var cell in nearCells)
            {
                cell.handleShot();
            }
        }
    }
}
