using Sit313assign1.Shared;
using Android.Content;
using Android.App;
using Android.OS;
using TaskyAndroid;
using Android.Widget;


namespace TaskyAndroid.Screens 
{

    /*
     * This activity is for the detail of mission,
     * users are able to create, edit, delete mission.
     */
	[Activity (Label = "Sit313assign1")]			
	public class MissionDetail : Activity 
	{
		Mission mission = new Mission();
		Button cancelOrDeleteButton;
		EditText descriptions;
        EditText deadline;
		CheckBox doneCheckbox;
        EditText missionName;
        Button saveButton;

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			
            //get task id from last screen by intent
			int taskID = Intent.GetIntExtra("TaskID", 0);
			if(taskID > 0) {
				mission = MissionDao.GetTask(taskID);
			}
			
            SetContentView(Resource.Layout.missionDetails);
			missionName = FindViewById<EditText>(Resource.Id.NameText);
			descriptions = FindViewById<EditText>(Resource.Id.DescriptionText);
            deadline = FindViewById<EditText>(Resource.Id.DeadlineText);
            saveButton = FindViewById<Button>(Resource.Id.SaveButton);

			doneCheckbox = FindViewById<CheckBox>(Resource.Id.chkDone);

            //show detail information according to mission id
            doneCheckbox.Checked = mission.Done;

			cancelOrDeleteButton = FindViewById<Button>(Resource.Id.CancelDeleteButton);

            //if user is creating a mission, then show cancel.
            cancelOrDeleteButton.Text = (mission.ID == 0 ? "Cancel" : "Delete");
			
			missionName.Text = mission.Name; 
			descriptions.Text = mission.Description;
            deadline.Text = mission.Deadline;

            cancelOrDeleteButton.Click += (sender, e) => { CancelDelete(); };
			saveButton.Click += (sender, e) => { Save(); };
		}

		void Save()
		{
			mission.Name = missionName.Text;
			mission.Description = descriptions.Text;
            mission.Deadline = deadline.Text;

            mission.Done = doneCheckbox.Checked;

			MissionDao.SaveTask(mission);
			Finish();
		}
		
		void CancelDelete()
		{
			if (mission.ID != 0) {
				MissionDao.DeleteTask(mission.ID);
			}
			Finish();
		}
	}
}