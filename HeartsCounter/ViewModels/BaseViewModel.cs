using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;

namespace HeartsCounter.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        public BaseViewModel ()
        {

        }

        [ObservableProperty]
        bool isBusy;
    }
}