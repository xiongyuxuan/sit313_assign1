using System;
using System.Collections.Generic;

namespace Sit313assign1.Shared 
{
	/*
     * This class is missin Database access object for the model: mission
     * It's an data access API 
     */
	public static class MissionDao 
	{
		static MissionDao ()
		{
		}
		
        //provide interface for controller
		public static Mission GetTask(int id)
		{
			return MissionDaoBridges.GetTask(id);
		}

        //provide interface for controller
        public static IList<Mission> GetTasks ()
		{
			return new List<Mission>(MissionDaoBridges.GetTasks());
		}

        //provide interface for controller
        public static int SaveTask (Mission item)
		{
			return MissionDaoBridges.SaveTask(item);
		}

        //provide interface for controller
        public static int DeleteTask(int id)
		{
			return MissionDaoBridges.DeleteTask(id);
		}
	}
}