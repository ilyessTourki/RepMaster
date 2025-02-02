using System;
using TrainSheet.Model.ServiceModel;
using TrainSheet.Service;

namespace TrainSheet.Utilities
{
	public static class Utilities
	{
		//SQLite Data Access
		public static string SQLiteDataAccessPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "permission.db3");
		public static SQLiteDataAccess<MuscleCategory> muscleCategDB = new SQLiteDataAccess<MuscleCategory>();
    }
}

