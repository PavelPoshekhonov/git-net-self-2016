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
            InitializeComponent();
        }

        private void btNewGame_Click(object sender, EventArgs e)
        {
            // Уничтожить контроллер игры (если он есть)
            if (this.GameController != null)
            {
                KeyDown -= this.GameController.KeyDown;
                ((IDisposable)this.GameController).Dispose();
            }

            // Создать контроллер игры
            GameController = new PackmanController(pnMap, 5, 5, 1);
            KeyDown += GameController.KeyDown; // Обработчик события нажатия на кнопку

            // Запустить игру
            GameController.Play();
        }

        private void btPause_Click(object sender, EventArgs e)
        {
            if (GameController.GameRunning == true)
            {
                GameController.Pause();
            }
            else
            {
                GameController.Play();
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