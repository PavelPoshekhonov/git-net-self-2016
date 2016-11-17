using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TankWars
{
    public partial class FormMain : Form
    {
        public PackmanController GameController;

        public FormMain(string[] args)
        {
            int mapWidth = 800;
            int mapHeight = 600;
            int tankAmount = 5;
            int appleAmount = 5;
            int mDelay = 10;

            InitializeComponent();

            // Разбор аргументов командной строки
            if (args.Length >= 1)   // Размеры игрового поля (ширина и высота)
            {
                try
                {
                }
                catch (IndexOutOfRangeException)
                {
                }
            }

            if (args.Length >= 2)   // Количество танков на поле
                int.TryParse(args[1], out tankAmount);

            if (args.Length >= 3)   // Количество яблок на поле
                int.TryParse(args[2], out appleAmount);

            if (args.Length >= 3)   // Скорость передвижения объектов (задается для всех объектов сразу)
                int.TryParse(args[2], out mDelay);

            // Задать размер игрового поля
            pnMap.Size = new Size(mapWidth, mapHeight);
            pnBottom.Size = new Size(mapWidth, pnBottom.Size.Height);
            pnBottom.Location = new Point(pnBottom.Location.X, mapHeight + 28);
            Size = new Size(mapWidth + 30, mapHeight + 106);

            // Создать контроллер игры
            GameController = new PackmanController(pnMap, lbLifes, lbApples, lbTanks, tankAmount, appleAmount, mDelay);
            KeyDown += GameController.KeyDown; // Обработчик события нажатия на кнопку

            // Запустить игру
            GameController.Run();
        }

        private void btNewGame_Click(object sender, EventArgs e)
        {
            GameController.Pause();
            GameController.InitNewGame();
            GameController.Run();
        }

        private void btPause_Click(object sender, EventArgs e)
        {
            if (GameController.GameRunning == true)
            {
                GameController.Pause();
                btPause.Text = "Resume";
            }
            else
            {
                GameController.Run();
                btPause.Text = "Pause";
            }
        }

        // Для проталкивания событий нажатия на кнопки-стрелки
        private void bt_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    e.IsInputKey = true;
                    break;
            }
        }
    }
}