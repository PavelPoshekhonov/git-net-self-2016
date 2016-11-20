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
    public partial class FormStat : Form
    {
        // Ссылка на контроллер игры, в которос содержатся игровые объекты
        PackmanController controller;

        // Отображение игровых объектов в таблице
        List<GameObjectViewTable> objectViewerTable = new List<GameObjectViewTable>();

        // Конструктор
        public FormStat()
        {
            InitializeComponent();

        }

        // Инициализация формы
        public void FormInit(PackmanController pc)
        {
            controller = pc;    // Сохраняем ссылку на контроллер

            int objCount = 0;

            // Создаем отображение игровых объектов в таблице
            objectViewerTable.Add(new GameObjectViewTable(dgvStat));
            // Назначаем обработчик события "Изменение положения"
            objectViewerTable[objCount++].SetLocationChangedHandler(controller.KolobokObject, objCount);

            // Инициализировать грид
            dgvStat.RowCount = objCount;
        }

        private void FormStat_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Назначаем обработчик события "Изменение положения"
            objectViewerTable[0].UnSetLocationChangedHandler(controller.KolobokObject);
        }
    }
}
