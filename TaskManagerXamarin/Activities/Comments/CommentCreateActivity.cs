using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.ComponentModel;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid.Activities.Comments
{
    [Activity(Label = "Create Comment", Icon = "@drawable/addComment")]
    public class CommentCreateActivity : Activity
    {
        private BaseServicesOf_CommentModelClient client;

        int taskID;
        EditText text;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            WebServiceClientManager.InitializeWebServiceClient(typeof(CommentModel));
            client = WebServiceClientManager.CommentClient;

            taskID = Intent.GetIntExtra("taskID", 0);

            SetContentView(Resource.Layout.CreateComment);

            text = FindViewById<EditText>(Resource.Id.commentEdit);
            Button save = FindViewById<Button>(Resource.Id.saveComment);

            save.Click += SaveButtonOnClick;
        }

        // SAVE
        private void SaveButtonOnClick(object sender, EventArgs e)
        {
            CommentModel comment = new CommentModel
            {
                Text = text.Text,
                CreateDate = DateTime.Now,
                TaskID = taskID,
                UserID = AuthenticationService.LoggedUser.ID
            };

            client.SaveAsync(comment);
            client.SaveCompleted += SaveButtonCompleted;
        }

        private void SaveButtonCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Finish();
        }
    }
}