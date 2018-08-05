using System.Collections.Generic;
using Android.App;
using Android.Widget;
using Sit313assign1.Shared;

namespace TaskyAndroid.ApplicationLayer 
{

	public class MissionAdapter : BaseAdapter<Mission> 
	{
		Activity context = null;
		IList<Mission> tasks = new List<Mission>();
		
		public MissionAdapter (Activity context, IList<Mission> tasks) : base ()
		{
			this.context = context;
			this.tasks = tasks;
		}
		
		public override Mission this[int position]
		{
			get { return tasks[position]; }
		}
		
		public override long GetItemId (int position)
		{
			return position;
		}
		
		public override int Count
		{
			get { return tasks.Count; }
		}
		
		public override Android.Views.View GetView (int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
		{
			var item = tasks[position];			
            
			var view = (convertView ??
				context.LayoutInflater.Inflate(
					Android.Resource.Layout.SimpleListItemChecked,
					parent,
					false)) as CheckedTextView;
			view.SetText (item.Name==""?"<new mission>":item.Name, TextView.BufferType.Normal);
			view.Checked = item.Done;
            
			return view;
		}
	}
}