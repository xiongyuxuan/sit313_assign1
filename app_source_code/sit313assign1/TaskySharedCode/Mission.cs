using System;

namespace Sit313assign1.Shared 
{
	public class Mission 
	{
		public Mission ()
		{
		}

        public int ID { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
        public string Deadline { get; set; }

        public bool Done { get; set; }	
	}
}