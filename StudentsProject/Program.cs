﻿using System;

namespace StudentsProject
{
	class MainClass
	{

		static void Main()
		{
			dbOptions DB = new dbOptions();
			DB.FindStudent("nika", "jajanidze");
		
		}
	}

}