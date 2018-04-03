using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FitnessMembership.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace FitnessMembership.ViewModel
{
		/// <summary>
		/// The VM for modifying or removing users.
		/// </summary>
		public class ChangeViewModel : ViewModelBase
		{
				/// <summary>
				/// The currently entered first name in the change window.
				/// </summary>
				private string enteredFName;

				/// <summary>
				/// The currently entered last name in the change window.
				/// </summary>
				private string enteredLName;

				/// <summary>
				/// The currently entered email in the change window.
				/// </summary>
				private string enteredEmail;

				/// <summary>
				/// The command that triggers saving the filled out member data.
				/// </summary>
				public RelayCommand<Window> ChangeCommand { get; private set; }

				/// <summary>
				/// The command that triggers closing the add window.
				/// </summary>
				public RelayCommand<Window> CancelCommand { get; private set; }

				/// <summary>
				/// Initializes a new instance of the ChangeViewModel class.
				/// </summary>
				public ChangeViewModel()
				{
						ChangeCommand = new RelayCommand<Window>(ChangeMethod);
						CancelCommand = new RelayCommand<Window>(CancelMethod);
						Messenger.Default.Register<Member>(this, GetSelected);
				}

				/// <summary>
				/// The currently entered first name in the change window.
				/// </summary>
				public string EnteredFName
				{
						get
						{
								return enteredFName;
						}
						set
						{
								enteredFName = value;
								RaisePropertyChanged("EnteredFName");
						}
				}

				/// <summary>
				/// The currently entered last name in the change window.
				/// </summary>
				public string EnteredLName
				{
						get
						{
								return enteredLName;
						}
						set
						{
								enteredLName = value;
								RaisePropertyChanged("EnteredLName");
						}
				}

				/// <summary>
				/// The currently entered email name in the change window.
				/// </summary>
				public string EnteredEmail
				{
						get
						{
								return enteredEmail;
						}
						set
						{
								enteredEmail = value;
								RaisePropertyChanged("EnteredEmail");
						}
				}

				/// <summary>
				/// Sends a valid member to the main VM to replace at the selected index with, then closes the change window.
				/// </summary>
				/// <param name="window">The window to close.</param>
				public void ChangeMethod(Window window)
				{
						try
						{
								var message = new MessageMember()
								{
										FirstName = enteredFName,
										LastName = enteredLName,
										Email = enteredEmail,
										Message = "Change"
								};
								Messenger.Default.Send(message);
								EnteredFName = null;
								EnteredLName = null;
								EnteredEmail = null;
								window.Close();
						}
						catch (ArgumentException)
						{
								MessageBox.Show("Fields must be under 25 characters.", "Entry Error");
						}
						catch (NullReferenceException)
						{
								MessageBox.Show("Fields cannot be empty.", "Entry Error");
						}
						catch (FormatException)
						{
								MessageBox.Show("Must be a valid e-mail address.", "Entry Error");
						}
				}

				/// <summary>
				/// Closes the window
				/// <param name="window">The window to close.</param>
				/// </summary>
				public void CancelMethod(Window window)
				{
						if (window != null)
						{
								window.Close();
						}
				}

				/// <summary>
				/// Receives a member from the main VM to auto-fill the change box with the currently selected member.
				/// </summary>
				/// <param name="m">The member data to fill in.</param>
				public void GetSelected(Member m)
				{
						if (m != null)
						{
								EnteredFName = m.FirstName;
								EnteredLName = m.LastName;
								EnteredEmail = m.Email;
						}
				}
		}
}