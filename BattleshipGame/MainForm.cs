using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BattleshipGame
{
    public partial class MainForm : Form
    {
        Model model;

        enum GameStatus {none, placing, playing, finished }

        int pickedShipSize = 4;
        Ship.Direction pickedShipDirection = Ship.Direction.horizontal;

        GameStatus status = GameStatus.none;

        int[] shipsRemaining;
        Label[] shipLabels;
        public MainForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            MouseMove += MainForm_MouseMove;            
            Paint += MainForm_Paint;

            PlayerFieldPictureBox.MouseMove += MainForm_MouseMove;
            PlayerFieldPictureBox.Paint += PictureBox_Paint;
            

            PlayerFieldPictureBox.Width = Globals.fieldSize * Globals.cellSize;
            PlayerFieldPictureBox.Height = Globals.fieldSize * Globals.cellSize;

            AIFieldPictureBox.Width = Globals.fieldSize * Globals.cellSize;
            AIFieldPictureBox.Height = Globals.fieldSize * Globals.cellSize;

            Init();

            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            model = new Model();
            

            Redraw();
        }

        private void Init()
        {
            shipLabels = new Label[5] { null, ShipLabel1, ShipLabel2, ShipLabel3, ShipLabel4 };
            shipsRemaining = new int[5] { 0, 4, 3, 2, 1 };
            status = GameStatus.none;

            for (int i = 1; i < 5; i++)
            {
                shipLabels[i].Text = String.Format("{0} - палубный, осталось: {1}", i, shipsRemaining[i]);
            }
        }

        private void NullShipCounters()
        {
            shipsRemaining = new int[5] { 0, 0, 0, 0, 0 };

            for (int i = 1; i < 5; i++)
            {
                shipLabels[i].Text = String.Format("{0} - палубный, осталось: {1}", i, 0);
            }

        }
        public void Redraw()
        {
            bool f = status == GameStatus.placing ? true : false;
            model.playerField.GenerateBitmap(pickedShipDirection, f);
            model.AIField.GenerateBitmap();
            PlayerFieldPictureBox.Image = model.playerField.bmp;
            AIFieldPictureBox.Image = model.AIField.bmp;
        }

        private void PlayerFieldPictureBox_Click(object sender, EventArgs e)
        {
            if (status != GameStatus.placing) return;

            var me = e as MouseEventArgs;
            if (me.Button == MouseButtons.Left)
            {
                int x = me.X / Globals.cellSize;
                int y = me.Y / Globals.cellSize;
                var cell = model.playerField.Field[y, x];
                bool success = model.playerField.PlaceShip(cell, pickedShipSize, pickedShipDirection);

                if (success)
                {
                    model.playerField.PossibleToPlaceShipCheck(pickedShipSize);
                    shipsRemaining[pickedShipSize]--;
                    shipLabels[pickedShipSize].Text = String.Format("{0} - палубный, осталось: {1}", pickedShipSize, shipsRemaining[pickedShipSize]);
                    if (shipsRemaining[pickedShipSize] <= 0) status = GameStatus.none;

                    Redraw();

                    int sum = 0;
                    foreach (int count in shipsRemaining)
                    {
                        sum += count;
                    }
                    if (sum == 0)
                    {
                        StartGame();
                    }
                }

            }
            else if (me.Button == MouseButtons.Right)
            {
                if (pickedShipDirection == Ship.Direction.horizontal)
                    pickedShipDirection = Ship.Direction.vertical;
                else
                    pickedShipDirection = Ship.Direction.horizontal;

                Redraw();
            }

        }

        private void AIFieldPictureBox_Click(object sender, EventArgs e)
        {
            if (status != GameStatus.playing) return;

            var me = e as MouseEventArgs;
            if (me.Button == MouseButtons.Left)
            {
                int x = me.X / Globals.cellSize;
                int y = me.Y / Globals.cellSize;

                var cell = model.AIField.Field[y, x];
                if (cell.handleShot() != Cell.TurnResult.fail)
                {
                    if (model.AIField.CountAliveShips() == 0)
                    {
                        status = GameStatus.finished;
                        StatusLabel.Text = "Вы победили!";
                    }
                    model.AITurn();

                    if (model.playerField.CountAliveShips() == 0)
                    {
                        status = GameStatus.finished;
                        StatusLabel.Text = "Вы проиграли";
                    }

                    Redraw();
                }
            }
        }

        private void DrawPickedShip(Point cursor, Graphics g)
        {
            if (status != GameStatus.placing) return;

            int xOffset = Globals.cellSize / 2;
            int yOffset = Globals.cellSize / 2;

            var brush = new SolidBrush(Color.Black);

            if (pickedShipDirection == Ship.Direction.horizontal)
                g.FillRectangle(brush, cursor.X - xOffset, cursor.Y - yOffset, pickedShipSize * Globals.cellSize, Globals.cellSize);
            if (pickedShipDirection == Ship.Direction.vertical)
                g.FillRectangle(brush, cursor.X - xOffset, cursor.Y - yOffset, Globals.cellSize, pickedShipSize * Globals.cellSize);

        }

        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Point local = this.PointToClient(Cursor.Position);
            DrawPickedShip(local, e.Graphics);
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            Point local = this.PlayerFieldPictureBox.PointToClient(Cursor.Position);
            DrawPickedShip(local, e.Graphics);
        }


        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            PlayerFieldPictureBox.Invalidate();
            Invalidate();
        }

        private void StartGame()
        {
            status = GameStatus.playing;
            StatusLabel.Text = "Игра началась!";
            StatusLabel.ForeColor = Color.DarkSeaGreen;
            MessageBox.Show("Корабли расставлены, начинаем игру");
        }

        void Pick(int size)
        {
            if (shipsRemaining[size] <= 0) return;
            model.playerField.PossibleToPlaceShipCheck(size);
            status = GameStatus.placing;
            pickedShipSize = size;
            Redraw();
        }

        private void shipLabel4_Click(object sender, EventArgs e)
        {
            Pick(4);
        }

        private void ShipLabel3_Click(object sender, EventArgs e)
        {
            Pick(3);
        }

        private void ShipLabel2_Click(object sender, EventArgs e)
        {
            Pick(2);
        }

        private void ShipLabel1_Click(object sender, EventArgs e)
        {
            Pick(1);
        }

        private void RandomButton_Click(object sender, EventArgs e)
        {
            model.playerField = new PlayerField();
            model.playerField.PlaceShipsRandomly();

            if (status == GameStatus.playing || status == GameStatus.finished) //если игра уже идет, сбросим поле противника
            {
                model.AIField = new PlayerField();
                model.AIField.PlaceShipsRandomly();
            }           

            NullShipCounters();
            Redraw();
            StartGame();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            model.playerField = new PlayerField();
            if (status == GameStatus.playing || status == GameStatus.finished) //если игра уже идет, сбросим поле противника
            {
                model.AIField = new AIField();
            }
            Init();
            Redraw();
        }
    }
}
