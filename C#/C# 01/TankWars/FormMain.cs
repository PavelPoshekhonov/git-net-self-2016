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
            PackmanController GameController = new PackmanController(pnMap, 5, 5, 200);
            KeyDown += GameController.KeyDown; // Обработчик события нажатия на кнопку

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

    }
}
