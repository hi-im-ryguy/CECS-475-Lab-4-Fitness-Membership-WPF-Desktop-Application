using System;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
//using Microsoft.Practices.ServiceLocation;

namespace FitnessMembership.ViewModel
{
		/// <summary>
		/// This class contains static references to all the view models in the
		/// application and provides an entry point for the bindings.
		/// </summary>
		public class ViewModelLocator
		{
				/// <summary>
				/// Initializes a new instance of the ViewModelLocator class.
				/// </summary>
				public ViewModelLocator()
				{
						ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
						SimpleIoc.Default.Register<MainViewModel>();
						SimpleIoc.Default.Register<AddViewModel>();
						SimpleIoc.Default.Register<ChangeViewModel>();
				}
				/// <summary>
				/// A property that lets the main window connect with its View Model.
				/// </summary>
				public MainViewModel MainViewModel
				{
						get { return ServiceLocator.Current.GetInstance<MainViewModel>(); }
				}

				/// <summary>
				/// A property that lets the add window connect with its View Model.
				/// </summary>
				public AddViewModel AddViewModel
				{
						get { return ServiceLocator.Current.GetInstance<AddViewModel>(); }
				}

				/// <summary>
				/// A property that lets the change window connect with its View Model.
				/// </summary>
				public ChangeViewModel ChangeViewModel
				{
						get { return ServiceLocator.Current.GetInstance<ChangeViewModel>(); }
				}
		}
}