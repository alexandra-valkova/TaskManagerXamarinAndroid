using System;
using Android.Views;
using Android.Widget;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid.Activities.Tasks
{
    internal class ResponsibleSpinnerAdapter : BaseAdapter<UserModel>
    {
        private int simpleSpinnerItem;
        private TaskEditActivity taskEditActivity;
        private UserModel[] users;

        public ResponsibleSpinnerAdapter(TaskEditActivity taskEditActivity, int simpleSpinnerItem, UserModel[] users)
        {
            this.taskEditActivity = taskEditActivity;
            this.simpleSpinnerItem = simpleSpinnerItem;
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
            View view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(taskEditActivity).Inflate(simpleSpinnerItem, null, false);
            }

            view.FindViewById<TextView>(Android.Resource.Layout.SimpleSpinnerDropDownItem).Text = users[position].Username;

            return view;
        }
    }
}