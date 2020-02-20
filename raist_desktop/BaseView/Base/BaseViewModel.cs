using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace raist_desktop.BaseView.Base
{
    class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {


        };

        public void OnPropertyChanged(String name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}
