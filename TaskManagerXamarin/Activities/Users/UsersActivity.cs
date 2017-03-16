using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using System;
using TaskManagerAndroid.ListAdapters.Users;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid.Activities.Users
{
    [Activity(Label = "Users", Icon = "@drawable/users")]
    public class UsersActivity : ListActivity
    {
        private BaseServicesOf_UserModelClient client;

        private UserModel[] users;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            WebServiceClientManager.InitializeWebServiceClient(typeof(UserModel));
            client = WebServiceClientManager.UserClient;

            SetContentView(Resource.Layout.Users);

            PopulateListView();

            Button create = FindViewById<Button>(Resource.Id.createUser);

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
            StartActivity(typeof(UserEditActivity));
        }

        // LIST VIEW
        protected void PopulateListView()
        {
            client.GetAllAsync();
            client.GetAllUsersCompleted += ListViewPopulatedCompleted;
        }

        private void ListViewPopulatedCompleted(object sender, GetAllUsersCompletedEventArgs getAllUsersCompletedEventArgs)
        {
            users = getAllUsersCompletedEventArgs.Result;
            RunOnUiThread(() => ListAdapter = new UsersListAdapter(this, users));
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            UserModel user = users[position];

            Intent edit = new Intent(this, typeof(UserEditActivity));
            edit.PutExtra("userID", user.ID);
            StartActivity(edit);
        }
    }
}