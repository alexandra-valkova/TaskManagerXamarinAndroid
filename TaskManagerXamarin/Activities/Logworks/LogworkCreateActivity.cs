using Android.App;
using Android.OS;
using Android.Widget;
using System;
using System.ComponentModel;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid.Activities.Logworks
{
    [Activity(Label = "Create Logwork", Icon = "@drawable/addLogwork")]
    public class LogworkCreateActivity : Activity
    {
        private BaseServicesOf_LogworkModelClient client;

        int taskID;
        EditText workingHours;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            WebServiceClientManager.InitializeWebServiceClient(typeof(LogworkModel));
            client = WebServiceClientManager.LogworkClient;

            taskID = Intent.GetIntExtra("taskID", 0);

            SetContentView(Resource.Layout.CreateLogwork);

            workingHours = FindViewById<EditText>(Resource.Id.logworkEdit);
            Button save = FindViewById<Button>(Resource.Id.saveLogwork);

            save.Click += SaveButtonOnClick;
        }

        // SAVE
        private void SaveButtonOnClick(object sender, EventArgs e)
        {
            LogworkModel logwork = new LogworkModel
            {
                WorkingHours = int.Parse(workingHours.Text),
                CreateDate = DateTime.Now,
                TaskID = taskID,
                UserID = AuthenticationService.LoggedUser.ID
            };

            client.SaveAsync(logwork);
            client.SaveCompleted += SaveLogworkCompleted;
        }

        private void SaveLogworkCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Finish();
        }
    }
}