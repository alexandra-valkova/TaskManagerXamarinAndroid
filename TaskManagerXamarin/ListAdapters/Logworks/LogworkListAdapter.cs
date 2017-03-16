using Android.Views;
using Android.Widget;
using TaskManagerAndroid.Activities.Logworks;
using TaskManagerApiWCF.Models;

namespace TaskManagerAndroid.ListAdapters.Logworks
{
    internal class LogworkListAdapter : BaseAdapter<LogworkModel>
    {
        private LogworkModel[] logworks;
        private LogworksActivity logworksActivity;

        public LogworkListAdapter(LogworksActivity logworksActivity, LogworkModel[] logworks)
        {
            this.logworksActivity = logworksActivity;
            this.logworks = logworks;
        }

        public override LogworkModel this[int position]
        {
            get { return logworks[position]; }
        }

        public override int Count
        {
            get { return logworks.Length; }
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
                view = logworksActivity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, null);
            }

            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = logworks[position].WorkingHours.ToString() + " " + (logworks[position].WorkingHours > 1 ? "hours" : "hour");
            view.FindViewById<TextView>(Android.Resource.Id.Text2).Text = logworks[position].CreateDate.ToString();

            return view;
        }
    }
}