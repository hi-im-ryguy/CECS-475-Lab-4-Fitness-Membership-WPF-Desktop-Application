using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using FitnessMembership.Model;
using FitnessMembership.View;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace FitnessMembership.ViewModel
{
		/// <summary>
		/// The VM for the main screen that shows the member list.
		/// </summary>
		public class MainViewModel : ViewModelBase
		{
				/// <summary>
				/// The list of registered members.
				/// </summary>
				private ObservableCollection<Member> members;

				/// <summary>
				/// The currently selected member.
				/// </summary>
				private Member selectedMember;

				/// <summary>
				/// Initializes a new instance of the MainViewModel class.
				/// </summary>

				/// <summary>
				/// The command that triggers adding a new member.
				/// </summary>
				public RelayCommand AddCommand { get; set; }

				/// <summary>
				/// The command that triggers adding a new member.
				/// </summary>
				public RelayCommand DeleteCommand { get; set; }

				/// <summary>
				/// The command that triggers adding a new member.
				/// </summary>
				public RelayCommand<Window> ExitCommand { get; private set; }

				/// <summary>
				/// The command that triggers adding a new member.
				/// </summary>
				public RelayCommand ChangeCommand { get; set; }

				public MainViewModel()
				{
						selectedMember = new Member();
						members = MemberDatabase.GetMemberships();
						AddCommand = new RelayCommand(AddMethod);
						DeleteCommand = new RelayCommand(DeleteMethod);
						ExitCommand = new RelayCommand<Window>(ExitMethod);
						ChangeCommand = new RelayCommand(ChangeMethod);
						Messenger.Default.Register<MessageMember>(this, ReceiveMember);
						//Messenger.Default.Register<NotificationMessage>(this, ReceiveMessage);
				}

				/// <summary>
				/// The currently selected member in the list box.
				/// </summary>
				public Member SelectedMember
				{
						get
						{
								return selectedMember;
						}
						set
						{
								selectedMember = value;
								RaisePropertyChanged("SelectedMember");
						}
				}

				/// <summary>
				/// Shows a new add screen.
				/// </summary>
				public void AddMethod()
				{
						AddWindow addWindow = new AddWindow();
						addWindow.Show();
				}

				/// <summary>
				/// Sends out a message to delete the selected member.
				/// </summary>
				public void DeleteMethod()
				{
						try
						{
								var message = new MessageMember()
								{
										FirstName = selectedMember.FirstName,
										LastName = selectedMember.LastName,
										Email = selectedMember.Email,
										Message = "Delete"
								};
								Messenger.Default.Send(message);
						}
						catch {}
				}

				/// <summary>
				/// Closes the application.
				/// </summary>
				/// <param name="window">The window to close.</param>
				public void ExitMethod(Window window)
				{
						if (window != null)
						{
								window.Close();
						}
				}
				/// <summary>
				/// Opens the change window.
				/// </summary>
				public void ChangeMethod()
				{
						if (SelectedMember != null)
						{
								ChangeWindow changeWindow = new ChangeWindow();
								Messenger.Default.Send(SelectedMember);
								changeWindow.ShowDialog();
								//var message = new MessageMember()
								//{
								//		FirstName = entered
								//}
								//Messenger.Default.Send();
						}
				}
				/// <summary>
				/// Gets a new member for the list.
				/// </summary>
				/// <param name="m">The member to add. The message denotes how it is added.
				/// "Update" replaces at the specified index, "Add" adds it to the list.</param>
				public void ReceiveMember(MessageMember m)
				{
						if (m.Message == "Add")
						{
								members.Add(m);
								MemberDatabase.SaveMemberships(members);
								MessageBox.Show($"{m.FirstName} {m.LastName} is now a Fitness member!");
						}
						else if (m.Message == "Change")
						{
								Member temp = SelectedMember;
								members[members.IndexOf(SelectedMember)] = m;
								MemberDatabase.SaveMemberships(members);
								MessageBox.Show($"{temp.FirstName} {temp.LastName} - {temp.Email} was changed to {m.FirstName} {m.LastName} - {m.Email}!");
						}
						
						else if (m.Message == "Delete")
						{
								members.Remove(SelectedMember); // Fill in
								MemberDatabase.SaveMemberships(members);
								MessageBox.Show($"{m.FirstName} {m.LastName} has been removed.");
						}
				}

				///// <summary>
				///// Gets text messages.
				///// </summary>
				///// <param name="msg">The received message. "Delete" means the currently selected member is deleted.</param>
				//public void ReceiveMessage(NotificationMessage msg)
				//{
						
				//}

				/// <summary>
				/// The list of registered members.
				/// </summary>
				public ObservableCollection<Member> MemberList
				{
						get { return members; }
				}
		}
}