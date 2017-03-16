using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.ComponentModel;
using TaskManagerAndroid.Activities.Comments;
using TaskManagerAndroid.Activities.Logworks;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid.Activities.Tasks
{
    [Activity(Label = "Edit Task", Icon = "@drawable/editTask")]
    public class TaskEditActivity : Activity
    {
        private BaseServicesOf_TaskModelClient client;
        private BaseServicesOf_UserModelClient clientUser;

        int taskID;
        TaskModel taskModel = null;
        UserModel[] users;

        EditText title;
        EditText description;
        EditText workingHours;
        //Spinner responsible;
        Spinner status;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EditTask);

            taskID = Intent.GetIntExtra("taskID", 0);

            WebServiceClientManager.InitializeWebServiceClient(typeof(TaskModel));
            client = WebServiceClientManager.TaskClient;

            WebServiceClientManager.InitializeWebServiceClient(typeof(UserModel));
            clientUser = WebServiceClientManager.UserClient;

            if (taskID > 0)
            {
                client.GetByIDAsync(taskID);
                client.GetByIDCompleted += GetByIDTaskCompleted;
            }

            clientUser.GetAllAsync();
            clientUser.GetAllUsersCompleted += GetAllUsersCompleted;

            title = FindViewById<EditText>(Resource.Id.titleEdit);
            description = FindViewById<EditText>(Resource.Id.descriptionEdit);
            workingHours = FindViewById<EditText>(Resource.Id.workingHoursEdit);
            //responsible = FindViewById<Spinner>(Resource.Id.responsibleEdit);
            status = FindViewById<Spinner>(Resource.Id.statusEdit);

            Button save = FindViewById<Button>(Resource.Id.saveTask);
            Button delete = FindViewById<Button>(Resource.Id.deleteTask);
            Button details = FindViewById<Button>(Resource.Id.detailsTask);
            Button logworks = FindViewById<Button>(Resource.Id.logworksTask);
            Button comments = FindViewById<Button>(Resource.Id.commentsTask);
            Button cancel = FindViewById<Button>(Resource.Id.cancelTask);

            save.Click += SaveButtonOnClick;
            delete.Click += DeleteButtonOnClick;
            details.Click += DetailsButtonOnClick;
            logworks.Click += LogworksButtonOnClick;
            comments.Click += CommentsButtonOnClick;
            cancel.Click += delegate
            {
                Finish();
            };

            // responsible spinner
            //responsible.ItemSelected += ResponsibleItemSelected;
            //ArrayAdapter responsibleAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, users);
            //responsibleAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            //responsible.Adapter = responsibleAdapter;
            //responsible.Adapter = new ResponsibleSpinnerAdapter(this, Android.Resource.Layout.SimpleSpinnerItem, users);

            // status spinner
            status.ItemSelected += StatusItemSelected;
            ArrayAdapter statusAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.status_array, Android.Resource.Layout.SimpleSpinnerItem);
            statusAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            status.Adapter = statusAdapter;
        }

        // Responsible
        private void ResponsibleItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            int userID = users[e.Position].ID;
            taskModel.ResponsibleID = userID;
        }

        // Status
        private void StatusItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            string status = spinner.GetItemAtPosition(e.Position).ToString();
            if (taskModel != null)
            {
                taskModel.Status = (StatusEnum)Enum.Parse(typeof(StatusEnum), status);
            }
        }

        // Details
        private void DetailsButtonOnClick(object sender, EventArgs e)
        {
            Intent detailsActivity = new Intent(this, typeof(TaskDetailsActivity));
            detailsActivity.PutExtra("taskID", taskID);
            StartActivity(detailsActivity);
        }

        // Logworks
        private void LogworksButtonOnClick(object sender, EventArgs e)
        {
            Intent logworksActivity = new Intent(this, typeof(LogworksActivity));
            logworksActivity.PutExtra("taskID", taskID);
            StartActivity(logworksActivity);
        }

        // Comments
        private void CommentsButtonOnClick(object sender, EventArgs e)
        {
            Intent commentsActivity = new Intent(this, typeof(CommentsActivity));
            commentsActivity.PutExtra("taskID", taskID);
            StartActivity(commentsActivity);
        }

        // SAVE
        private void SaveButtonOnClick(object sender, EventArgs e)
        {
            if (taskModel == null)
            {
                taskModel = new TaskModel();
                taskModel.CreateDate = DateTime.Now;
                taskModel.CreatorID = AuthenticationService.LoggedUser.ID;
                taskModel.ResponsibleID = AuthenticationService.LoggedUser.ID;
            }

            taskModel.Title = title.Text;
            taskModel.Description = description.Text;
            taskModel.WorkingHours = int.Parse(workingHours.Text);
            taskModel.LastEditDate = DateTime.Now;

            client.SaveAsync(taskModel);
            client.SaveCompleted += SaveUserCompleted;
        }

        private void SaveUserCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Finish();
        }

        // DELETE
        private void DeleteButtonOnClick(object sender, EventArgs e)
        {
            client.DeleteAsync(taskModel);
            client.DeleteCompleted += DeleteUserCompleted;
        }

        private void DeleteUserCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Finish();
        }

        // Get By ID
        private void GetByIDTaskCompleted(object sender, GetTaskByIDCompletedEventArgs getTaskByIDCompletedEventArgs)
        {
            taskModel = getTaskByIDCompletedEventArgs.Result;

            RunOnUiThread(() =>
            {
                title.Text = taskModel.Title;
                description.Text = taskModel.Description;
                workingHours.Text = taskModel.WorkingHours.ToString();
                status.SetSelection((int)taskModel.Status);
            });
        }

        // Get All Users
        private void GetAllUsersCompleted(object sender, GetAllUsersCompletedEventArgs getAllUsersCompletedEventArgs)
        {
            users = getAllUsersCompletedEventArgs.Result;
        }
    }
}