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
    public class MemberList : ObservableObject
    {
				/// <summary>
				/// The list of members.
				/// </summary>
				ObservableCollection<Member> memberList;

				/// <summary>
				/// Keeps track of the number of Members in the MemberList.
				/// </summary>
				private int count;

				/// <summary>
				/// Constructors
				/// </summary>
				public MemberList() {
						memberList = new ObservableCollection<Member>();
						count = 0;
				}			

				public MemberList(ObservableCollection<Member> memberList)
				{
						this.memberList = memberList;
						foreach (Member member in memberList) {
								this.count++;
						}
				}

				/// <summary>
				/// Overloaded addition operator to add a Member to a MemberList. MemberList (+) Member
				/// </summary>
				/// <param name="m1"></param>
				/// <param name="m2"></param>
				/// <returns></returns>
				public static MemberList operator +(MemberList m1, Member m2)
				{
						MemberList temp = m1;
						temp.Add(m2);
						temp.count++;
						return temp;
				}

				/// <summary>
				/// Overloaded subtraction operator to remove a Member from a MemberList. MemberList (-) Member
				/// </summary>
				/// <param name="m1"></param>
				/// <param name="m2"></param>
				/// <returns></returns>
				public static MemberList operator -(MemberList m1, Member m2)
				{
						MemberList temp = m1;
						temp.Remove(m2);
						temp.count--;
						return temp;
				}

				/// <summary>
				/// Adds a Member onto the tail of the MemberList, using an overloaded operator +.
				/// </summary>
				/// <param name="addingMember"></param>
				public MemberList Add(Member addingMember) 
				{
						MemberList temp = new MemberList()
						{
								memberList = this.memberList,
								count = this.count
						};
						temp = temp + addingMember;
						return temp;
				}

				/// <summary>
				/// Removes Member from MemberList, using an overloaded operator -.
				/// </summary>
				/// <param name="removingMember"></param>
				public MemberList Remove(Member removingMember)
				{
						MemberList temp = new MemberList()
						{
								memberList = this.memberList,
								count = this.count
						};
						temp = temp - removingMember;
						return temp;
				}

				/// <summary>
				/// Gets the Members from a file containing all Members from the Database.
				/// </summary>
				public void Write()
				{
						ObservableCollection<Member> fileList = MemberDatabase.GetMemberships();
						foreach (Member member in fileList)
						{
								memberList.Add(member);
						}
				}

				/// <summary>
				/// Saves Member List onto Member Database.
				/// </summary>
				public void Save()
				{
						MemberDatabase.SaveMemberships(memberList);
				}


    }
}
