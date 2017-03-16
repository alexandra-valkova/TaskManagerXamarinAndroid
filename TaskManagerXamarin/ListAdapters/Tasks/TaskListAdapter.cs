using Android.Views;
using Android.Widget;
using TaskManagerAndroid.Activities.Tasks;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid.ListAdapters.Tasks
{
    internal class TaskListAdapter : BaseAdapter<TaskModel>
    {
        private TaskModel[] tasks;
        private TasksActivity tasksActivity;

        public TaskListAdapter(TasksActivity tasksActivity, TaskModel[] tasks) : base()
        {
            this.tasksActivity = tasksActivity;
            this.tasks = tasks;
        }

        public override TaskModel this[int position]
        {
            get { return tasks[position]; }
        }

        public override int Count
        {
            get { return tasks.Length; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; // re-use an existing view, if one is avaible

            if (view == null)       // otherwise create a new one
            {
                view = tasksActivity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            }

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = tasks[position].Title;
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = tasks[position].Description;

            return view;
        }
    }
}