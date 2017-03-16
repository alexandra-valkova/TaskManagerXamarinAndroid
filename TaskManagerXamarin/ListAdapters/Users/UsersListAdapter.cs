using Android.App;
using Android.Views;
using Android.Widget;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid.ListAdapters.Users
{
    public class UsersListAdapter : BaseAdapter<UserModel>
    {
        UserModel[] users;
        Activity usersActivity;

        public UsersListAdapter(Activity usersActivity, UserModel[] users) : base()
        {
            this.usersActivity = usersActivity;
            this.users = users;
        }

        public override UserModel this[int position]
        {
            get { return users[position]; }
        }

        public override int Count
        {
            get { return users.Length; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is available

            if (view == null)       // otherwise create a new one
            {
                view = usersActivity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            }

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = users[position].Username;
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = users[position].FirstName + " " + users[position].LastName;

            return view;
        }
    }
}