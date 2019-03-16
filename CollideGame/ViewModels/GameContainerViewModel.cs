using CollideGameTestClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using System.Collections.ObjectModel;
using CollideGameTestClient.Entities;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;

namespace CollideGame.ViewModels
{
    public class GameContainerViewModel : ViewModelBase
    {
        public Game Game { get; set; }
        public string StatusMessage { get; set; }
        public ObservableCollection<EntityBase> GameEntities { get; set; }
        public double GameHeight { get; set; }
        public double GameWidth { get; set; }
        public ICommand GenerateCommand { get; set; }
        public GameContainerViewModel()
        {
            GameEntities = new ObservableCollection<EntityBase>();
            StatusMessage = "Start";
            GenerateCommand = new ActionCommand(Generate);
            Game = new Game();
            Task.Factory.StartNew(() =>
           {
               Game.Start();

           });
            StatusLoop();
            foreach (var entity in Game.Entities)
                GameEntities.Add(entity);
            GameHeight = Game.GameArea.Height;
            GameWidth = Game.GameArea.Width;
        }

        private void Generate()
        {
            if (Game != null)
            {
                Game.GenerateRandomEntity();
                GameEntities.Clear();
                foreach (var entity in Game.Entities)
                    GameEntities.Add(entity);
            }
        }

        void StatusLoop()
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (Game != null)
                        StatusMessage = Game.StatusMessage;
                }

            });
        }
    }
}
