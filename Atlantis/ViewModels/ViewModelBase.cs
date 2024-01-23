using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.ViewModels
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string porpertyName) { 
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(porpertyName));
        }
         
    }
}
