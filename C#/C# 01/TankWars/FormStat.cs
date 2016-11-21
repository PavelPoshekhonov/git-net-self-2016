using System.Collections.Generic;
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

        // Инициализация окна
        public void FormInit(PackmanController pc)
        {
            controller = pc;    // Сохраняем ссылку на контроллер

            int objCount = 0;

            // Инициализировать грид
            dgvStat.RowCount = 1 + controller.AppleList.Count + controller.TankList.Count + controller.BulletList.Count;
            dgvStat.Height =  (dgvStat.RowCount + 1) * dgvStat.RowTemplate.Height - 2;
            Height = dgvStat.Height +  51;

            objectViewerTable.Clear();

            // Создаем отображение игровых объектов в таблице
            objectViewerTable.Add(new GameObjectViewTable(dgvStat));
            // Назначаем обработчик события "Изменение положения"
            objectViewerTable[objCount].SetEventHandlers(controller.KolobokObject, objCount++);

            foreach (Apple app in controller.AppleList)
            {
                // Создаем отображение игровых объектов в таблице
                objectViewerTable.Add(new GameObjectViewTable(dgvStat));
                // Назначаем обработчик события "Изменение положения"
                objectViewerTable[objCount].SetEventHandlers(app, objCount++);
            }

            foreach (Tank tnk in controller.TankList)
            {
                // Создаем отображение игровых объектов в таблице
                objectViewerTable.Add(new GameObjectViewTable(dgvStat));
                // Назначаем обработчик события "Изменение положения"
                objectViewerTable[objCount].SetEventHandlers(tnk, objCount++);
            }

            foreach (Bullet blt in controller.BulletList)
            {
                // Создаем отображение пуль в таблице
                objectViewerTable.Add(new BulletViewTable(dgvStat));
                // Назначаем обработчик события "Изменение положения"
                objectViewerTable[objCount].SetEventHandlers(blt, objCount++);
            }
        }


        // Закрытие окна
        private void FormStat_FormClosing(object sender, FormClosingEventArgs e)
        {
            int objCount = 0;

            // Отменяем обработчик события "Изменение положения"
            objectViewerTable[objCount++].UnSetEventHandlers(controller.KolobokObject);

            foreach (Apple app in controller.AppleList)
            {
                // Отменяем обработчик события "Изменение положения"
                objectViewerTable[objCount++].UnSetEventHandlers(app);
            }

            foreach (Tank tnk in controller.TankList)
            {
                // Отменяем обработчик события "Изменение положения"
                objectViewerTable[objCount++].UnSetEventHandlers(tnk);
            }

            foreach (Bullet blt in controller.BulletList)
            {
                // Отменяем обработчик события "Изменение положения"
                objectViewerTable[objCount++].UnSetEventHandlers(blt);
            }
        }
    }
}
