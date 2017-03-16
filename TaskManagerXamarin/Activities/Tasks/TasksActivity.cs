using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using System.Linq;
using TaskManagerAndroid.ListAdapters.Tasks;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid.Activities.Tasks
{
    [Activity(Label = "Tasks", Icon = "@drawable/tasksList")]
    public class TasksActivity : ListActivity
    {
        private BaseServicesOf_TaskModelClient client;

        private TaskModel[] tasks;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            WebServiceClientManager.InitializeWebServiceClient(typeof(TaskModel));
            client = WebServiceClientManager.TaskClient;

            SetContentView(Resource.Layout.Tasks);

            PopulateListView();

            Button create = FindViewById<Button>(Resource.Id.createTask);

            create.Click += CreateButtonOnClick;
        }

        protected override void OnResume()
        {
            base.OnResume();

            PopulateListView();
        }

        // Create
        private void CreateButtonOnClick(object sender, EventArgs e)
        {
            StartActivity(typeof(TaskEditActivity));
        }

        // LIST VIEW
        private void PopulateListView()
        {
            client.GetAllAsync();
            client.GetAllCompleted += ListViewPopulatedCompleted;
        }

        private void ListViewPopulatedCompleted(object sender, GetAllTasksCompletedEventArgs getAllTasksCompletedEventArgs)
        {
            tasks = getAllTasksCompletedEventArgs.Result.Where(t => t.CreatorID == AuthenticationService.LoggedUser.ID || t.ResponsibleID == AuthenticationService.LoggedUser.ID).ToArray();
            RunOnUiThread(() => ListAdapter = new TaskListAdapter(this, tasks));
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            TaskModel task = tasks[position];

            Intent edit = new Intent(this, typeof(TaskEditActivity));
            edit.PutExtra("taskID", task.ID);
            StartActivity(edit);
        }
    }
}