using System.Collections.ObjectModel;
using System.Windows;
using GongSolutions.Wpf.DragDrop;

namespace GC.MV
{
    public class DragDropVm : IDropTarget
    {
        public ObservableCollection<ItemViewModel> Items;
        public void DragOver(IDropInfo dropInfo)
        {
            var sourceItem = dropInfo.Data as ItemViewModel;
            var targetItem = dropInfo.TargetItem as ItemViewModel;

            if (sourceItem != null && targetItem != null && targetItem.CanAcceptChildren)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Copy;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            throw new System.NotImplementedException();
        }
    }

    public class ItemViewModel
    {
        public bool CanAcceptChildren { get; set; }
        public ObservableCollection<ItemViewModel> Children { get; private set; }
    }
}
