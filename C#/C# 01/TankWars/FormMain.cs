using System;
using System.Drawing;
using System.Windows.Forms;

namespace TankWars
{
    public partial class FormMain : Form
    {
        public PackmanController GameController;
        FormStat frm; // Окно статистики

        public FormMain(string[] args)
        {
            InitializeComponent();

            int mapWidth = 600;
            int mapHeight = 500;
            int tankAmount = 5;
            int appleAmount = 5;
            int moveDelay = 10;


            // Разбор аргументов командной строки
            if (args.Length >= 1)   // Размеры игрового поля (ширина и высота)
            {
                string[] split = args[0].Split(new char[] { 'x', 'X', 'х', 'Х' }, StringSplitOptions.RemoveEmptyEntries);
                if (split.Length == 2)
                {
                    int.TryParse(split[0], out mapWidth);
                    int.TryParse(split[1], out mapHeight);
                    mapWidth = Math.Min(mapWidth, 1800);
                    mapHeight = Math.Min(mapHeight, 1000);
                    mapWidth = Math.Max(mapWidth, 500);
                    mapHeight = Math.Max(mapHeight, 500);
                }
            }

            if (args.Length >= 2)   // Количество танков на поле
            {
                int.TryParse(args[1], out tankAmount);
                tankAmount = Math.Min(tankAmount, 20);
            }

            if (args.Length >= 3)   // Количество яблок на поле
            {  
                int.TryParse(args[2], out appleAmount);
                appleAmount = Math.Min(appleAmount, 20);
            }

            if (args.Length >= 3)   // Скорость передвижения объектов (задается для всех объектов сразу)
            {
                int speed;
                int.TryParse(args[3], out speed);
                switch (speed)
                {
                    case 1:
                        {
                            moveDelay = 50;
                            break;
                        }
                    case 2:
                        {
                            moveDelay = 25;
                            break;
                        }
                    case 3:
                    default:
                        {
                            moveDelay = 10;
                            break;
                        }
                }
            }

            // Задать размер игрового поля
            pnMap.Size = new Size(mapWidth, mapHeight);
            pnBottom.Size = new Size(mapWidth, pnBottom.Size.Height);
            pnBottom.Location = new Point(pnBottom.Location.X, mapHeight + 28);
            pnHelp.Size = new Size(mapWidth, pnBottom.Size.Height);
            pnHelp.Location = new Point(pnBottom.Location.X, mapHeight + 65);
            Size = new Size(mapWidth + 40, mapHeight + 144);

            // Создать контроллер игры
            GameController = new PackmanController(pnMap, lbLifes, lbApples, lbTanks, tankAmount, appleAmount, moveDelay);
            KeyDown += GameController.KeyDown; // Обработчик события нажатия на кнопку

            // Запустить игру
            GameController.Run();
        }

        // Новая игра
        private void btNewGame_Click(object sender, EventArgs e)
        {
            pnMap.Select(); // Убираем фокус с кнопки
            GameController.Pause();
            GameController.InitNewGame();
            GameController.Run();
        }

        // Пауза
        private void btPause_Click(object sender, EventArgs e)
        {
            pnMap.Select(); // Убираем фокус с кнопки
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

        // Статистика
        private void btStat_Click(object sender, EventArgs e)
        {
            pnMap.Select(); // Убираем фокус с кнопки

            if ((frm == null) || (frm.IsDisposed == true))
            {
                frm = new FormStat(); // Создать окно
                frm.FormInit(GameController);
            }
            frm.Show();

            Activate();// Вернуться на главную форму
            pnMap.Select(); // Убираем фокус с кнопки
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            KeyDown -= GameController.KeyDown; // Обработчик события нажатия на кнопку
            GameController.Dispose();
        }
    }
}