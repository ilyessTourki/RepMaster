using System;
using TrainSheet.Model.ServiceModel;
using TrainSheet.Service;

namespace TrainSheet.Utilities
{
	public static class Utilities
	{
		//SQLite Data Access
		public static string SQLiteDataAccessPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "permission.db3");
		public static SQLiteDataAccess<MuscleCategory>	exercicesDB	= new SQLiteDataAccess<MuscleCategory>();
        public static SQLiteDataAccess<User>			userDB		= new SQLiteDataAccess<User>();
        public static SQLiteDataAccess<BodyParts>		bodyPartsDB = new SQLiteDataAccess<BodyParts>();
    }
}

