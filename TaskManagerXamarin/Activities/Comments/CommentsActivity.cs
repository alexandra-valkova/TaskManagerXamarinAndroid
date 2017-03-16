using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using System;
using System.Linq;
using TaskManagerAndroid.ListAdapters.Comments;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid.Activities.Comments
{
    [Activity(Label = "Comments", Icon = "@drawable/comments")]
    public class CommentsActivity : ListActivity
    {
        private BaseServicesOf_CommentModelClient client;

        int taskID;
        private CommentModel[] comments;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            WebServiceClientManager.InitializeWebServiceClient(typeof(CommentModel));
            client = WebServiceClientManager.CommentClient;

            taskID = Intent.GetIntExtra("taskID", 0);

            SetContentView(Resource.Layout.Comments);

            PopulateListView();

            Button create = FindViewById<Button>(Resource.Id.createComment);

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
            Intent create = new Intent(this, typeof(CommentCreateActivity));
            create.PutExtra("taskID", taskID);
            StartActivity(create);
        }

        // LIST VIEW
        private void PopulateListView()
        {
            client.GetAllAsync();
            client.GetAllCompleted += ListViewPopulatedCompleted;
        }

        private void ListViewPopulatedCompleted(object sender, GetAllCommentsCompletedEventArgs getAllCommentsCompletedEventArgs)
        {
            comments = getAllCommentsCompletedEventArgs.Result.Where(c => c.TaskID == taskID).ToArray();
            RunOnUiThread(() => ListAdapter = new CommentListAdapter(this, comments));
        }
    }
}