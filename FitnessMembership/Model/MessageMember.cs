using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessMembership.Model
{
		/// <summary>
		/// An extension of member that also includes a message for some sort of extra description.
		/// </summary>
		public class MessageMember : Member
		{
				public MessageMember() { }

				/// <summary>
				/// Creates a new member.
				/// </summary>
				/// <param name="firstName">The member's first name.</param>
				/// <param name="lastName">The member's last name.</param>
				/// <param name="email">The member's email.</param>
				/// <param name="message">The extra description</param>
				public MessageMember(string firstName, string lastName, string email, string message) : base(firstName, lastName, email)
				{
						Message = message;
				}

				/// <summary>
				/// A property that includes the message.
				/// </summary>
				public string Message { get; set; }
		}
}