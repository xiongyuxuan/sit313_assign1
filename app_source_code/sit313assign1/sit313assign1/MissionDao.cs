using System;
using System.Collections.Generic;

namespace Sit313assign1.Shared 
{
	
	public static class MissionDao 
	{
		static MissionDao ()
		{
		}
		
		public static Mission GetTask(int id)
		{
			return MissionDaoBridges.GetTask(id);
		}
		
		public static IList<Mission> GetTasks ()
		{
			return new List<Mission>(MissionDaoBridges.GetTasks());
		}
		
		public static int SaveTask (Mission item)
		{
			return MissionDaoBridges.SaveTask(item);
		}
		
		public static int DeleteTask(int id)
		{
			return MissionDaoBridges.DeleteTask(id);
		}
	}
}