using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeartsCounter.Services.Interfaces.CrossPlatform
{
    public interface IBottomSheetDialogService
    {
        void ShowBottomSheet(Page page, IView bottomSheetContent, bool dimDismiss);
        void HideCurrentBottomSheet();
    }
}
