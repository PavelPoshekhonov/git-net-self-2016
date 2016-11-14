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
        protected PictureBox picBox = new PictureBox();

        public PackmanController GameController;

        public FormMain(string[] args)
        {
            InitializeComponent();

            // Test
            // Нарисуем picBox
            picBox.Height = 25;
            picBox.Width = 25;
            picBox.Location = new Point(50, 50);

            Bitmap kolobok = new Bitmap(25, 25);
            Graphics kolobokGraphics = Graphics.FromImage(kolobok);
            int yellow = 0;
            int white = 11;
            while (white <= 25)
            {
                kolobokGraphics.FillRectangle(Brushes.Yellow, 0, yellow, 25, 5);
                kolobokGraphics.FillRectangle(Brushes.White, 0, white, 25, 5);
                yellow += 5;
                white += 5;
            }
            picBox.Image = kolobok;

            pnMap.Controls.Add(picBox);
        }

        private void btNewGame_Click(object sender, EventArgs e)
        {
            // Уничтожить контроллер игры (если он есть) и создать его заново
            this.GameController?.Dispose();
            PackmanController GameController = new PackmanController(pnMap, 5, 5, 200);
            // Запустить игру
            GameController.Play();
        }

        // Вспомогательные функции для реакции на нажатие стрелок
        protected override bool IsInputKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Right:
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                    return true;
                case Keys.Shift | Keys.Right:
                case Keys.Shift | Keys.Left:
                case Keys.Shift | Keys.Up:
                case Keys.Shift | Keys.Down:
                    return true;
            }
            return base.IsInputKey(keyData);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    if (e.Shift)
                    {

                    }
                    else
                    {
                    }
                    break;
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    {
                        picBox.Location = new Point(picBox.Location.X, picBox.Location.Y - 1);
                        break;
                    }
                case Keys.Down:
                    {
                        picBox.Location = new Point(picBox.Location.X, picBox.Location.Y + 1);
                        break;
                    }
                case Keys.Left:
                    {
                        picBox.Location = new Point(picBox.Location.X - 1, picBox.Location.Y);
                        break;
                    }
                case Keys.Right:
                    {
                        picBox.Location = new Point(picBox.Location.X + 1, picBox.Location.Y);
                        break;
                    }
                default:
                    break;
            }
        }

    }
}
