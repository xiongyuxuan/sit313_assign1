using Android.App;
using System.Collections.Generic;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using TaskyAndroid.ApplicationLayer;
using Sit313assign1.Shared;
using Android.Widget;
using TaskyAndroid;


namespace TaskyAndroid.Screens 
{
	
	[Activity (Label = "Sit313assign1",  
		MainLauncher = true,
        Icon = "@drawable/icon",
        ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
		)]
	public class MainActivity : Activity 
	{
		MissionAdapter taskListAdapter;
		IList<Mission> mission;
		Button addTaskButton;
		ListView taskListView;
		
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            
			SetContentView(Resource.Layout.MainScreen);
            
			taskListView = FindViewById<ListView> (Resource.Id.TaskList);
			addTaskButton = FindViewById<Button> (Resource.Id.AddButton);
            
			if(addTaskButton != null) {
				addTaskButton.Click += (sender, e) => {
					StartActivity(typeof(MissionDetail));
				};
			}
			
			if(taskListView != null) {
				taskListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
					var missionDetails = new Intent (this, typeof (MissionDetail));
					missionDetails.PutExtra ("TaskID", mission[e.Position].ID);
					StartActivity (missionDetails);
				};
			}
		}
		
		protected override void OnResume ()
		{
			base.OnResume ();

			mission = MissionDao.GetTasks();
			
			taskListAdapter = new MissionAdapter(this, mission);
            
			taskListView.Adapter = taskListAdapter;
		}
	}
}