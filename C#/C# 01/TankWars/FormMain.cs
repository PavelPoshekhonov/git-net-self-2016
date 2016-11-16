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
            int mapWidth;
            int mapHeight;
            int tankAmount;
            int appleAmount;
            int mDelay;

            InitializeComponent();

            // Разбор аргументов командной строки
            mapWidth = 500;
            mapHeight = 500;
            tankAmount = 5;
            appleAmount = 5;
            mDelay = 10;

            // Задать размер игрового поля
            pnMap.Size = new Size(mapWidth, mapHeight);
            pnBottom.Size = new Size(mapWidth, pnBottom.Size.Height);
            pnBottom.Location = new Point(pnBottom.Location.X, mapHeight + 28);
            Size = new Size(mapWidth + 30, mapHeight + 96);

            // Создать контроллер игры
            GameController = new PackmanController(pnMap, tankAmount, appleAmount, mDelay);
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