﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismHelperApp.ViewModels
{
    public class ViewModelBase : ReactiveObject, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }


        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
