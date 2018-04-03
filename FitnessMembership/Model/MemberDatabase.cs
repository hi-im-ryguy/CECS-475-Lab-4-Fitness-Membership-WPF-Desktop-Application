using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessMembership.Model
{
		/// <summary>
		/// A class that uses a text file to store information about the gym members longterm.
		/// </summary>
		public class MemberDatabase : ObservableObject
		{
				/// <summary>
				/// The list of members to be saved.
				/// </summary>
				private ObservableCollection<Member> members;

				/// <summary>
				/// Where the database is stored.
				/// </summary>
				private const string filepath = "../members.txt";

				public MemberDatabase() { }

				/// <summary>
				/// Creates a new member database.
				/// </summary>
				/// <param name="m">The list to saved from or written to.</param>
				public MemberDatabase(ObservableCollection<Member> m)
				{
						members = m;
				}

				/// <summary>
				/// Reads the saved text file database into the program's list of members.
				/// </summary>
				/// <returns>The list containing the text file data read in.</returns>
				public static ObservableCollection<Member> GetMemberships()
				{
						try
						{
								StreamReader input = new StreamReader(new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Read));
								ObservableCollection<Member> membersList = new ObservableCollection<Member>();
								while(input.Peek() != -1)
								{
										string row = input.ReadLine();
										row = row.Replace("-", "");
										string[] column = row.Split(' ');
										membersList.Add(new Member(column[0], column[1], column[3]));
								}								
								input.Close();
								return membersList;
						}
						catch (FileNotFoundException)
						{
								Console.WriteLine("File not found.");
						}
						catch (FormatException)
						{
					  Console.WriteLine("Invalid e-mail address format.");
						}
						return null;
				}

				/// <summary>
				/// Saves the program's list of members into the text file database.
				/// </summary>
				public static void SaveMemberships(ObservableCollection<Member> savingList)
				{
						StreamWriter output = new StreamWriter(new FileStream(filepath, FileMode.Create, FileAccess.Write));
						foreach(Member member in savingList)
						{
								output.WriteLine(member.GetDisplayText());
						}
						output.Close();
				}
		}
}