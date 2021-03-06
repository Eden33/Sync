﻿using Model.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using Client.Resource;

using Client.UI.Collection;
using Client.UI.Input;
using Client.UI.Proxy;

namespace Client
{
    public class MainViewModel
    {

        public MainViewModel()
        {
            List<UICollectionPoint> cps = resourceMgr.GetAllItems<UICollectionPoint>();
            foreach (UICollectionPoint c in cps)
            {
                collectionPointList.Add(c);
            }
        }

        #region members

        private ResourceManager resourceMgr = ResourceManager.Instance;
        private UICollectionPoint selectedCollectionPoint;

        #endregion

        #region Properties

        public UICollectionPoint SelectedCollectionPoint
        {
            get 
            { 
                return selectedCollectionPoint; 
            }
            set 
            { 
                selectedCollectionPoint = value;
                DeleteCommand.SetCanExecute(value != null);
                LockCommand.SetCanExecute(value != null);
                UnlockCommand.SetCanExecute(value != null);
            }
        }

        #endregion

        #region Collections

        private ObservableCollectionEx<UICollectionPoint> collectionPointList = new ObservableCollectionEx<UICollectionPoint>();
        public ObservableCollectionEx<UICollectionPoint> CollectionPointList
        {
            get { return collectionPointList; }
        }

        #endregion

        #region Commands
        private Command deleteCommand;
        public Command DeleteCommand
        {
            get { return deleteCommand ?? (deleteCommand = new Command(DeleteCollectionPointFromRAM)); }
        }

        private void DeleteCollectionPointFromRAM()
        {
            if(selectedCollectionPoint != null)
            {
                selectedCollectionPoint.Deleted = true;
            }
        }

        private Command lockCommand;
        public Command LockCommand
        {
            get { return lockCommand ?? (lockCommand = new Command(TryLockOnServer));  }
        }

        private void TryLockOnServer()
        {
            resourceMgr.RequestLock<UICollectionPoint>(selectedCollectionPoint.Id);
        }

        private Command unlockCommand;

        public Command UnlockCommand
        {
            get { return unlockCommand ?? (unlockCommand = new Command(UnlockOnServer)); }
        }

        private void UnlockOnServer()
        {
            resourceMgr.Unlock<UICollectionPoint>(selectedCollectionPoint.Id);
        }

        #endregion
    }
}
