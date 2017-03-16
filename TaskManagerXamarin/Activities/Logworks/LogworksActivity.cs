using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Linq;
using TaskManagerAndroid.ListAdapters.Logworks;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid.Activities.Logworks
{
    [Activity(Label = "Logworks", Icon = "@drawable/logworks")]
    public class LogworksActivity : ListActivity
    {
        private BaseServicesOf_LogworkModelClient client;

        int taskID;
        private LogworkModel[] logworks;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            WebServiceClientManager.InitializeWebServiceClient(typeof(LogworkModel));
            client = WebServiceClientManager.LogworkClient;

            taskID = Intent.GetIntExtra("taskID", 0);

            SetContentView(Resource.Layout.Logworks);

            PopulateListView();

            Button create = FindViewById<Button>(Resource.Id.createLogwork);

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
            Intent create = new Intent(this, typeof(LogworkCreateActivity));
            create.PutExtra("taskID", taskID);
            StartActivity(create);
        }

        // LIST VIEW
        private void PopulateListView()
        {
            client.GetAllAsync();
            client.GetAllCompleted += ListViewPopulatedCompleted;
        }

        private void ListViewPopulatedCompleted(object sender, GetAllLogworksCompletedEventArgs getAllLogworksCompletedEventArgs)
        {
            logworks = getAllLogworksCompletedEventArgs.Result.Where(l => l.TaskID == taskID).ToArray();
            RunOnUiThread(() => ListAdapter = new LogworkListAdapter(this, logworks));
        }
    }
}