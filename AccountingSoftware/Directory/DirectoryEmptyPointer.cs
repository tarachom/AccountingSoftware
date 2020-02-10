using System;
using System.Collections.Generic;

namespace AccountingSoftware
{
	/// <summary>
	/// Пустий вказівник
	/// </summary>
	public class DirectoryEmptyPointer
	{
		public DirectoryEmptyPointer()
		{
			UnigueID = new UnigueID(Guid.Empty);
		}

		public UnigueID UnigueID { get; private set; }
	}
}
