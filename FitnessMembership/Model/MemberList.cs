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
				ObservableCollection<Member> memberList;
				private int count;

				public MemberList() {
						memberList = new ObservableCollection<Member>();
						count = 0;
				}			

				public void Add(Member addingMember) 
				{
						memberList.Add(addingMember);
						count++;
				}

				public void Remove(Member removingMember)
				{
						memberList.Remove(removingMember);
						count--;
				}

				public void Write()
				{
						ObservableCollection<Member> fileList = MemberDatabase.GetMemberships();
						foreach (Member member in fileList)
						{
								memberList.Add(member);
						}
				}

				public void Save()
				{
						MemberDatabase.SaveMemberships(memberList);
				}
    }
}
