using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollideGame.ViewModels
{
    public class ViewModelRegion
    {
        static ViewModelRegion()
        {
            GlobalRegion = new ViewModelRegion();
        }
        public ViewModelRegion()
        {
            Id = Guid.NewGuid().ToString();
        }
        public static readonly ViewModelRegion GlobalRegion;
        public string Id { get; set; }
    }
}
