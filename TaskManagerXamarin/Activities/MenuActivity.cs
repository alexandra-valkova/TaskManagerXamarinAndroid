using Android.App;
using Android.OS;
using Android.Widget;
using System;
using TaskManagerAndroid.Activities.Tasks;
using TaskManagerAndroid.Activities.Users;

namespace TaskManagerAndroid.Activities
{
    [Activity(Label = "Menu", Icon = "@drawable/menu")]
    public class MenuActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Menu);

            Button users = FindViewById<Button>(Resource.Id.usersMenu);
            Button tasks = FindViewById<Button>(Resource.Id.tasksMenu);

            users.Click += UsersButtonOnClick;
            tasks.Click += TasksButtonOnClick;
        }

        // Users
        private void UsersButtonOnClick(object sender, EventArgs eventArgs)
        {
            StartActivity(typeof(UsersActivity));
        }

        // Tasks
        private void TasksButtonOnClick(object sender, EventArgs e)
        {
            StartActivity(typeof(TasksActivity));
        }
    }
}